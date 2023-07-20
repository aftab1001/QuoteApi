using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Command;
using DocuWare.Application.Features.Quote.Dtos;
using DocuWare.Common.Constants;
using DocuWare.Domain.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace DocuWare.UnitTest.Application.Features.Command;

[TestFixture]
public class CreateQuoteCommandHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _quoteRepository = Substitute.For<IRepository<Quote>>();
        _characterByQuoteContentRepository = Substitute.For<ICharacterByQuoteContentRepository>();
        _movieByQuoteContentRepository = Substitute.For<IMovieByQuoteContentRepository>();
        _logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();
        _actorByQuoteContentRepository = Substitute.For<IActorByQuoteContentRepository>();

        systemUnderTest = new CreateQuoteCommandHandler(
            _quoteRepository,
            _characterByQuoteContentRepository,
            _movieByQuoteContentRepository,
            _logger,
            _actorByQuoteContentRepository);
    }

    private CreateQuoteCommandHandler systemUnderTest;
    private IRepository<Quote> _quoteRepository;
    private ICharacterByQuoteContentRepository _characterByQuoteContentRepository;
    private IMovieByQuoteContentRepository _movieByQuoteContentRepository;
    private ILogger<CreateQuoteCommandHandler> _logger;
    private IActorByQuoteContentRepository _actorByQuoteContentRepository;

    private CreateQuoteCommand SetupCommandWithException()
    {
        var command = new CreateQuoteCommand("Quote content");

        _movieByQuoteContentRepository.GetMovieByQuoteContent(command.Content).Returns(new[] {new Movie()});
        _characterByQuoteContentRepository.GetCharacterByQuoteContent(command.Content).Returns(new[] {new Character()});
        _actorByQuoteContentRepository.GetActorByQuoteContent(command.Content)
            .Throws(_ => throw new Exception("Something went wrong"));

        return command;
    }

    [Test]
    public async Task Handle_WhenAllAssignmentsSucceed_ReturnsSuccessResult()
    {
        var command = SetupCommandWithAllAssignments();

        var result = await InvokeCreateQuoteCommandHandler(command);

        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Message);
        await _quoteRepository.Received(1).SaveChangesAsync();
    }

    private Task<CreateQuoteResponseDto> InvokeCreateQuoteCommandHandler(CreateQuoteCommand command)
    {
        return systemUnderTest.Handle(command, CancellationToken.None);
    }

    private CreateQuoteCommand SetupCommandWithAllAssignments()
    {
        var command = new CreateQuoteCommand("Quote content");
        var movie = new Movie();
        var character = new Character();
        var actor = new Actor();


        _movieByQuoteContentRepository.GetMovieByQuoteContent(command.Content).Returns(new[] {movie});
        _characterByQuoteContentRepository.GetCharacterByQuoteContent(command.Content).Returns(new[] {character});
        _actorByQuoteContentRepository.GetActorByQuoteContent(command.Content).Returns(new[] {actor});
        _quoteRepository.Add(Arg.Do<Quote>(q => _ = q));
        return command;
    }

    [Test]
    public async Task Handle_WhenNoCharacterFound_ThrowsUserFriendlyException()
    {
        var command = SetupCommandWithNoCharacter();

        var result = await InvokeCreateQuoteCommandHandler(command);


        Assert.AreEqual(ErrorMessage.NoCharacterFound, result.Message);
        await _quoteRepository.DidNotReceive().SaveChangesAsync();
    }

    private CreateQuoteCommand SetupCommandWithNoCharacter()
    {
        var command = new CreateQuoteCommand("Quote content");


        _movieByQuoteContentRepository.GetMovieByQuoteContent(command.Content).Returns(new[] {new Movie()});
        _characterByQuoteContentRepository.GetCharacterByQuoteContent(command.Content)
            .Returns(Enumerable.Empty<Character>());
        return command;
    }

    [Test]
    public async Task Handle_WhenExceptionOccurs_ReturnsFailureResultWithMessage()
    {
        var command = SetupCommandWithException();


        var result = await InvokeCreateQuoteCommandHandler(command);


        Assert.IsFalse(result.Success);
        Assert.AreEqual("Something went wrong", result.Message);
        await _quoteRepository.DidNotReceive().SaveChangesAsync();
    }
}