using DocuWare.Application.Contracts;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByMovieQueryHandler : IRequestHandler<GetQuotesByMovieQuery, IEnumerable<Domain.Entities.Quote>>
{
    private readonly IQuoteByMovieRepository _quoteByMovieRepository;

    public GetQuotesByMovieQueryHandler(IQuoteByMovieRepository quoteByMovieRepository)
    {
        _quoteByMovieRepository = quoteByMovieRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Quote>> Handle(GetQuotesByMovieQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteByMovieRepository.GetQuotesByMovieAsync(request.MovieId);
        return quotes;
    }
}