using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByMovieQuery : IRequest<QuotesByMovieResponseDto>
{
    public GetQuotesByMovieQuery(int movieId)
    {
        MovieId = movieId;
    }

    public int MovieId { get; }
}