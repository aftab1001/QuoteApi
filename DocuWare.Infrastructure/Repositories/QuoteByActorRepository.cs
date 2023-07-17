using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class QuoteByActorRepository : IQuoteByActorRepository
{
    private readonly QuoteDbContext _context;

    public QuoteByActorRepository(QuoteDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Quote>> GetQuotesByActorAsync(int actorId)
    {
        return Task.FromResult<IEnumerable<Quote>>(
            _context.Quotes
                .Include(x => x.Characters).ThenInclude(y => y.Actor)
                .Where(q => q.Characters.Any(x => x.Actor.Id == actorId))
                .ToList());
    }
}