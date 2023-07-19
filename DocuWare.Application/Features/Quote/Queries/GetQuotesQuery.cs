using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesQuery : IRequest<QuotesResponseDto>
{
    // No additional parameters required
}