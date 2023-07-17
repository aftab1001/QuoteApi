using DocuWare.Domain.Entities;

namespace DocuWare.Application.Contracts;

public interface IQuoteByActorRepository
{
    Task<IEnumerable<Quote>> GetQuotesByActorAsync(int actorId);
}