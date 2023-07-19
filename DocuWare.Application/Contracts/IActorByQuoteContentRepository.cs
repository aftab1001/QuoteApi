using DocuWare.Domain.Entities;

namespace DocuWare.Application.Contracts;

public interface IActorByQuoteContentRepository
{
    Task<IEnumerable<Actor>> GetActorByQuoteContent(string content);
}