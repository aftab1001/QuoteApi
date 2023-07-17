using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand>
{
    private readonly IRepository<Character> _characterRepository;
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public CreateQuoteCommandHandler(IRepository<Domain.Entities.Quote> quoteRepository,
        IRepository<Movie> movieRepository,
        IRepository<Character> characterRepository)
    {
        _quoteRepository = quoteRepository;
        _movieRepository = movieRepository;
        _characterRepository = characterRepository;
    }

    public async Task<Unit> Handle(CreateQuoteCommand command, CancellationToken cancellationToken)
    {
        var quote = new Domain.Entities.Quote
        {
            Content = command.Content
        };

        _quoteRepository.Add(quote);
        await _quoteRepository.SaveChangesAsync();

        return Unit.Value;
    }
}