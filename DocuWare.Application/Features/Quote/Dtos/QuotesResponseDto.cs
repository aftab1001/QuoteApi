using AutoMapper;
using DocuWare.Application.Mappings;

namespace DocuWare.Application.Features.Quote.Dtos;

public class QuotesResponseDto : BaseResponseDto, IMapFrom<Domain.Entities.Quote>
{
    public List<QuoteResponse> Result { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Quote, QuoteResponse>();
        profile.CreateMap<List<Domain.Entities.Quote>, QuotesResponseDto>()
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
    }
}