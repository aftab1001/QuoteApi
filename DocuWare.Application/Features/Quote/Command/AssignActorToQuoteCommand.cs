using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class AssignActorToQuoteCommand : IRequest<AssignActorToQuoteResponseDto>
{
    public int QuoteId { get; set; }
    public int ActorId { get; set; }
}