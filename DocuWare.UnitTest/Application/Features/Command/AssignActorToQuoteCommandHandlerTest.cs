using System.Threading;
using System.Threading.Tasks;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Command;
using DocuWare.Application.Features.Quote.Dtos;
using DocuWare.Domain.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace DocuWare.UnitTest.Application.Features.Command;

[TestFixture]
public class AssignActorToQuoteCommandHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _quoteRepositoryMock = Substitute.For<IRepository<Quote>>();
        _actorRepositoryMock = Substitute.For<IRepository<Actor>>();
        _loggerMock = Substitute.For<ILogger<AssignActorToQuoteCommandHandler>>();

        systemUnderTest = new AssignActorToQuoteCommandHandler(_quoteRepositoryMock, _actorRepositoryMock, _loggerMock);
    }

    private AssignActorToQuoteCommandHandler systemUnderTest;
    private IRepository<Quote> _quoteRepositoryMock;
    private IRepository<Actor> _actorRepositoryMock;
    private ILogger<AssignActorToQuoteCommandHandler> _loggerMock;


    [Test]
    public async Task Handle_WhenQuoteAndActorExist_ReturnsSuccessResult()
    {
        var command = SetupCommandWithQuoteAndActor();

        var result = await InvokeAssignActorToQuoteCommandHandler(command);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Message);
        await _quoteRepositoryMock.Received(1).SaveChangesAsync();
    }

    private Task<AssignActorToQuoteResponseDto> InvokeAssignActorToQuoteCommandHandler(
        AssignActorToQuoteCommand command)
    {
        return systemUnderTest.Handle(command, CancellationToken.None);
    }

    private AssignActorToQuoteCommand SetupCommandWithQuoteAndActor()
    {
        var command = new AssignActorToQuoteCommand
        {
            QuoteId = 1,
            ActorId = 1
        };

        var quote = new Quote {Id = 1};
        var actor = new Actor {Id = 1};

        _quoteRepositoryMock.GetByIdAsync(command.QuoteId).Returns(quote);
        _actorRepositoryMock.GetByIdAsync(command.ActorId).Returns(actor);
        return command;
    }

    [Test]
    public async Task Handle_WhenQuoteNotFound_ReturnsFailureResult()
    {
        var command = SetupCommandWithNullQuote();
        var actual = await InvokeAssignActorToQuoteCommandHandler(command);

        Assert.IsFalse(actual.Success);
        Assert.AreEqual("Quote not found.", actual.Message);
        await _quoteRepositoryMock.DidNotReceive().SaveChangesAsync();
    }

    private AssignActorToQuoteCommand SetupCommandWithNullQuote()
    {
        // Arrange
        var command = new AssignActorToQuoteCommand
        {
            QuoteId = 1,
            ActorId = 1
        };

        _quoteRepositoryMock.GetByIdAsync(command.QuoteId)!.Returns((Quote) null);
        return command;
    }

    [Test]
    public async Task Handle_WhenActorNotFound_ReturnsFailureResult()
    {
        var command = SetupCommandWithOutActor();

        var result = await InvokeAssignActorToQuoteCommandHandler(command);

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Actor not found.", result.Message);
        await _quoteRepositoryMock.DidNotReceive().SaveChangesAsync();
    }

    private AssignActorToQuoteCommand SetupCommandWithOutActor()
    {
        var command = new AssignActorToQuoteCommand
        {
            QuoteId = 1,
            ActorId = 1
        };

        var quote = new Quote {Id = 1};

        _quoteRepositoryMock.GetByIdAsync(command.QuoteId).Returns(quote);
        _actorRepositoryMock.GetByIdAsync(command.ActorId)!.Returns((Actor) null!);
        return command;
    }
}