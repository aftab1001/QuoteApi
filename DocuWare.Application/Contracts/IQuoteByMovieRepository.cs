using DocuWare.Domain.Entities;

namespace DocuWare.Application.Contracts;

public interface IQuoteByMovieRepository
{
    Task<IEnumerable<Quote>> GetQuotesByMovieAsync(int movieId);
}