using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class QuoteByMovieRepository : IQuoteByMovieRepository
{
    private readonly QuoteDbContext _context;

    public QuoteByMovieRepository(QuoteDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Quote>> GetQuotesByMovieAsync(int movieId)
    {
        return Task.FromResult<IEnumerable<Quote>>(
            _context.Quotes
                .Include(x => x.Movies).Where(x => x.Movies.Any(movie => movie.Id == movieId)).ToList());
    }
}