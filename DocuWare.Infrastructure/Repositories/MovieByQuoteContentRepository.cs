using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class MovieByQuoteContentRepository : IMovieByQuoteContentRepository
{
    private readonly QuoteDbContext _context;

    public MovieByQuoteContentRepository(QuoteDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Movie>> GetMovieByQuoteContent(string content)
    {
        return Task.FromResult<IEnumerable<Movie>>(
            _context.Movies
                .Include(x => x.Quotes)
                .Where(q => q.Quotes.Any(x => x.Content == content))
                .ToList());
    }
}