using DocuWare.Domain.Entities;

namespace DocuWare.Application.Contracts;

public interface IMovieByQuoteContentRepository
{
    Task<IEnumerable<Movie>> GetMovieByQuoteContent(string content);
}