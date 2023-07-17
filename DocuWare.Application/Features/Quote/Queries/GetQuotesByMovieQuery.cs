using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByMovieQuery : IRequest<IEnumerable<Domain.Entities.Quote>>
{
    public GetQuotesByMovieQuery(int movieId)
    {
        MovieId = movieId;
    }

    public int MovieId { get; }
}