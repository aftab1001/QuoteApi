using DocuWare.Application.Features.Quote.Dtos;
using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class CreateQuoteCommand : IRequest<CreateQuoteResponseDto>
{
    public CreateQuoteCommand(string content)
    {
        Content = content;
    }

    public string Content { get; set; }
}