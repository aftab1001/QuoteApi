using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class CreateQuoteCommand : IRequest
{
    public CreateQuoteCommand(string content)
    {
        Content = content;
    }

    public string Content { get; set; }
}