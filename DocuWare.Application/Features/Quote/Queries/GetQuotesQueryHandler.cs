using AutoMapper;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, QuotesResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public GetQuotesQueryHandler(IRepository<Domain.Entities.Quote> quoteRepository, IMapper mapper)
    {
        _quoteRepository = quoteRepository;
        _mapper = mapper;
    }

    public async Task<QuotesResponseDto> Handle(GetQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetAllAsync();
        var result = _mapper.Map<QuotesResponseDto>(quotes);
        result.SetSuccess(true);
        return result;
    }
}