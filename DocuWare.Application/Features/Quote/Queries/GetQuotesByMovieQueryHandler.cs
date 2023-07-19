using AutoMapper;
using DocuWare.Application.Contracts;
using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Queries;

public class GetQuotesByMovieQueryHandler : IRequestHandler<GetQuotesByMovieQuery, QuotesByMovieResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IQuoteByMovieRepository _quoteByMovieRepository;


    public GetQuotesByMovieQueryHandler(IQuoteByMovieRepository quoteByMovieRepository, IMapper mapper)
    {
        _quoteByMovieRepository = quoteByMovieRepository;
        _mapper = mapper;
    }

    public async Task<QuotesByMovieResponseDto> Handle(GetQuotesByMovieQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteByMovieRepository.GetQuotesByMovieAsync(request.MovieId);
        var result = _mapper.Map<QuotesByMovieResponseDto>(quotes);
        result.SetSuccess(true);
        return result;
    }
}