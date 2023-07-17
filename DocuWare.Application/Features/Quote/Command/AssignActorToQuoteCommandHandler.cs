using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class AssignActorToQuoteCommandHandler : IRequestHandler<AssignActorToQuoteCommand>
{
    private readonly IRepository<Actor> _actorRepository;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public AssignActorToQuoteCommandHandler(IRepository<Domain.Entities.Quote> quoteRepository,
        IRepository<Actor> actorRepository)
    {
        _quoteRepository = quoteRepository;
        _actorRepository = actorRepository;
    }

    public async Task<Unit> Handle(AssignActorToQuoteCommand command, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetByIdAsync(command.QuoteId);
        if (quote == null) throw new Exception("Quote not found.");

        var actor = await _actorRepository.GetByIdAsync(command.ActorId);

        if (actor == null) throw new Exception("Actor not found.");

        await _quoteRepository.SaveChangesAsync();

        return Unit.Value;
    }
}