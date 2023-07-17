using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesQuery : IRequest<IEnumerable<Domain.Entities.Quote>>
{
    // No additional parameters required
}