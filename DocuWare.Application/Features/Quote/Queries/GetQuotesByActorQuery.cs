using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByActorQuery : IRequest<IEnumerable<Domain.Entities.Quote>>
{
    public GetQuotesByActorQuery(int actorId)
    {
        ActorId = actorId;
    }

    public int ActorId { get; }
}