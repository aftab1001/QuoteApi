using DocuWare.Application.Contracts;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByActorQueryHandler : IRequestHandler<GetQuotesByActorQuery, IEnumerable<Domain.Entities.Quote>>
{
   private readonly IQuoteByActorRepository _quoteByActorRepository;
    public GetQuotesByActorQueryHandler(IQuoteByActorRepository quoteByActorRepository)
    {
        _quoteByActorRepository = quoteByActorRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Quote>> Handle(GetQuotesByActorQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteByActorRepository.GetQuotesByActorAsync(request.ActorId);
        return quotes;
    }
}