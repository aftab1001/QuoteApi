using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class ActorByQuoteContentRepository : IActorByQuoteContentRepository
{
    private readonly QuoteDbContext _context;

    public ActorByQuoteContentRepository(QuoteDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Actor>> GetActorByQuoteContent(string content)
    {
        return Task.FromResult<IEnumerable<Actor>>(
            _context.Actors
                .Include(x => x.Quotes)
                .Where(q => q.Quotes.Any(x => x.Content == content))
                .ToList());
    }
}