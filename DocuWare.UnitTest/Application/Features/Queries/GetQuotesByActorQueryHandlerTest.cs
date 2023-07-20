using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Dtos;
using DocuWare.Application.Features.Quote.Queries;
using DocuWare.Domain.Entities;
using NSubstitute;
using NUnit.Framework;

namespace DocuWare.UnitTest.Application.Features.Queries;

[TestFixture]
public class GetQuotesByActorQueryHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _mapper = Substitute.For<IMapper>();
        _quoteByActorRepository = Substitute.For<IQuoteByActorRepository>();

        _handler = new GetQuotesByActorQueryHandler(_quoteByActorRepository, _mapper);
    }

    private GetQuotesByActorQueryHandler _handler;
    private IMapper _mapper;
    private IQuoteByActorRepository _quoteByActorRepository;


    [Test]
    public async Task Handle_WhenQuotesExist_ReturnsQuotesByActorResponseDto()
    {
        var query = SetupQueryWithQuote();
        var expectedResponse = new QuotesByActorResponseDto {Result = new List<QuoteResponse>()};
        _mapper.Map<QuotesByActorResponseDto>(Arg.Any<IEnumerable<Quote>>()).Returns(expectedResponse);

        var actual = await InvokeQueryWithQuote(query);


        Assert.AreEqual(expectedResponse.Result, actual.Result);
        Assert.IsTrue(actual.Success);
    }

    private Task<QuotesByActorResponseDto> InvokeQueryWithQuote(GetQuotesByActorQuery query)
    {
        return _handler.Handle(query, CancellationToken.None);
    }

    private GetQuotesByActorQuery SetupQueryWithQuote()
    {
        var query = new GetQuotesByActorQuery(1);

        var quotes = new List<Quote> {new() {Id = 1, Content = "Quote 1"}};
        _quoteByActorRepository.GetQuotesByActorAsync(query.ActorId).Returns(quotes);

        return query;
    }

    [Test]
    public async Task Handle_WhenQuotesDoNotExist_ReturnsEmptyQuotesByActorResponseDto()
    {
        var query = SetupQueryWithoutQuotes();
        var expected = new QuotesByActorResponseDto {Result = new List<QuoteResponse>()};

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.AreEqual(expected.Result, result.Result);
        Assert.IsTrue(result.Success);
    }

    private GetQuotesByActorQuery SetupQueryWithoutQuotes()
    {
        var query = new GetQuotesByActorQuery(1);

        var quotes = new List<Quote>();
        var expected = new QuotesByActorResponseDto {Result = new List<QuoteResponse>()};

        _quoteByActorRepository.GetQuotesByActorAsync(query.ActorId).Returns(quotes);
        _mapper.Map<QuotesByActorResponseDto>(quotes).Returns(expected);
        return query;
    }
}