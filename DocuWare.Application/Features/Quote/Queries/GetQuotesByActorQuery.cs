using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByActorQuery : IRequest<QuotesByActorResponseDto>
{
    public GetQuotesByActorQuery(int actorId)
    {
        ActorId = actorId;
    }

    public int ActorId { get; }
}