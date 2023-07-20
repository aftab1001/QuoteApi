using DocuWare.Domain.Entities;

namespace DocuWare.Application.Contracts;

public interface ICharacterByQuoteContentRepository
{
    Task<IEnumerable<Character>> GetCharacterByQuoteContent(string content);
}