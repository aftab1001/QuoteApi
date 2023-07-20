using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class CharacterByQuoteContentRepository : ICharacterByQuoteContentRepository
{
    private readonly QuoteDbContext _context;

    public CharacterByQuoteContentRepository(QuoteDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Character>> GetCharacterByQuoteContent(string content)
    {
        return Task.FromResult<IEnumerable<Character>>(
            _context.Characters
                .Include(x => x.Quotes)
                .Where(q => q.Quotes.Any(x => x.Content == content))
                .ToList());
    }
}