using AutoMapper;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByActorQueryHandler : IRequestHandler<GetQuotesByActorQuery, QuotesByActorResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IQuoteByActorRepository _quoteByActorRepository;

    public GetQuotesByActorQueryHandler(IQuoteByActorRepository quoteByActorRepository, IMapper mapper)
    {
        _quoteByActorRepository = quoteByActorRepository;
        _mapper = mapper;
    }

    public async Task<QuotesByActorResponseDto> Handle(GetQuotesByActorQuery request,
        CancellationToken cancellationToken)
    {
        var result = new QuotesByActorResponseDto();
        var quotes = await _quoteByActorRepository.GetQuotesByActorAsync(request.ActorId);
        result.SetSuccess(true);
        return _mapper.Map<QuotesByActorResponseDto>(quotes);
    }
}