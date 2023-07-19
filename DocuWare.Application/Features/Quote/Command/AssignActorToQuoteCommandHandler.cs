using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Dtos;
using DocuWare.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DocuWare.Application.Features.Quote.Command;

public class
    AssignActorToQuoteCommandHandler : IRequestHandler<AssignActorToQuoteCommand, AssignActorToQuoteResponseDto>
{
    private readonly IRepository<Actor> _actorRepository;
    private readonly ILogger<AssignActorToQuoteCommandHandler> _logger;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public AssignActorToQuoteCommandHandler(IRepository<Domain.Entities.Quote> quoteRepository,
        IRepository<Actor> actorRepository, ILogger<AssignActorToQuoteCommandHandler> logger)
    {
        _quoteRepository = quoteRepository;
        _actorRepository = actorRepository;
        _logger = logger;
    }

    public async Task<AssignActorToQuoteResponseDto> Handle(AssignActorToQuoteCommand command,
        CancellationToken cancellationToken)
    {
        var result = new AssignActorToQuoteResponseDto();

        var quote = await _quoteRepository.GetByIdAsync(command.QuoteId);

        if (quote == null)
        {
            result.Message = "Quote not found.";
            _logger.LogError(result.Message);
            return result;
        }

        var actor = await _actorRepository.GetByIdAsync(command.ActorId);

        if (actor == null)
        {
            result.Message = "Actor not found.";
            _logger.LogError(result.Message);
            return result;
        }

        quote.Actor = actor;
        await _quoteRepository.SaveChangesAsync();
        result.SetSuccess(true);


        return result;
    }
}