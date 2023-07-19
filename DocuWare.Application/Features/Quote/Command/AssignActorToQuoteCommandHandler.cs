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
        try
        {
            var quote = await _quoteRepository.GetByIdAsync(command.QuoteId);
            if (quote == null) throw new Exception("Quote not found.");

            var actor = await _actorRepository.GetByIdAsync(command.ActorId);

            if (actor == null) throw new Exception("Actor not found.");

            quote.Actor = actor;
            await _quoteRepository.SaveChangesAsync();
            result.SetSuccess(true);
        }
        catch (Exception exception)
        {
            result.Message = exception.Message;
            _logger.LogError(exception, exception.Message);
            throw;
        }

        return result;
    }
}