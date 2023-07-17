using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class AssignActorToQuoteCommand : IRequest
{
    public int QuoteId { get; set; }
    public int ActorId { get; set; }
}