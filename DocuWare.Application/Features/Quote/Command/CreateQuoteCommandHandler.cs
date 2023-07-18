using DocuWare.Application.Contracts;
using DocuWare.Application.Exceptions;
using DocuWare.Common.Constants;
using DocuWare.Domain.Entities;
using MediatR;

namespace DocuWare.Application.Features.Quote.Command;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand>
{
    private readonly ICharacterByQuoteContentRepository _characterByQuoteContentRepository;
    private readonly IMovieByQuoteContentRepository _movieByQuoteContentRepository;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public CreateQuoteCommandHandler(IRepository<Domain.Entities.Quote> quoteRepository,
        ICharacterByQuoteContentRepository characterByQuoteContentRepository,
        IMovieByQuoteContentRepository movieByQuoteContentRepository)
    {
        _quoteRepository = quoteRepository;
        _characterByQuoteContentRepository = characterByQuoteContentRepository;
        _movieByQuoteContentRepository = movieByQuoteContentRepository;
    }

    public async Task<Unit> Handle(CreateQuoteCommand command, CancellationToken cancellationToken)
    {
        var quote = new Domain.Entities.Quote
        {
            Content = command.Content
        };

        var movieByQuoteContent = await _movieByQuoteContentRepository.GetMovieByQuoteContent(command.Content);
        var movies = movieByQuoteContent.ToList();
        if (!movies.Any()) throw new UserFriendlyException(ErrorMessage.NoMovieFound);

        var characterByQuoteContent = await _characterByQuoteContentRepository.GetMovieByQuoteContent(command.Content);
        var characters = characterByQuoteContent as Character[] ?? characterByQuoteContent.ToArray();
        if (!characters.Any()) throw new UserFriendlyException(ErrorMessage.NoCharacterFound);

        quote.Character = characters.FirstOrDefault()!;
        quote.Movie = movies.FirstOrDefault()!;
        _quoteRepository.Add(quote);
        await _quoteRepository.SaveChangesAsync();

        return Unit.Value;
    }
}