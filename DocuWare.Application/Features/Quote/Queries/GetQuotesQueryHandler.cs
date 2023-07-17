using DocuWare.Application.Contracts;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, IEnumerable<Domain.Entities.Quote>>
{
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public GetQuotesQueryHandler(IRepository<Domain.Entities.Quote> quoteRepository)
    {
        _quoteRepository = quoteRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Quote>> Handle(GetQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetAllAsync();
        return await Task.FromResult(quotes);
    }
}