using DocuWare.Application.Contracts;
using DocuWare.Application.Exceptions;
using DocuWare.Application.Features.Quote.Dtos;
using DocuWare.Common.Constants;
using DocuWare.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DocuWare.Application.Features.Quote.Command;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, CreateQuoteResponseDto>
{
    private readonly ICharacterByQuoteContentRepository _characterByQuoteContentRepository;
    private readonly ILogger<CreateQuoteCommandHandler> _logger;
    private readonly IMovieByQuoteContentRepository _movieByQuoteContentRepository;
    private readonly IRepository<Domain.Entities.Quote> _quoteRepository;

    public CreateQuoteCommandHandler(IRepository<Domain.Entities.Quote> quoteRepository,
        ICharacterByQuoteContentRepository characterByQuoteContentRepository,
        IMovieByQuoteContentRepository movieByQuoteContentRepository, ILogger<CreateQuoteCommandHandler> logger)
    {
        _quoteRepository = quoteRepository;
        _characterByQuoteContentRepository = characterByQuoteContentRepository;
        _movieByQuoteContentRepository = movieByQuoteContentRepository;
        _logger = logger;
    }

    public async Task<CreateQuoteResponseDto> Handle(CreateQuoteCommand command, CancellationToken cancellationToken)
    {
        var result = new CreateQuoteResponseDto();
        try
        {
            var quote = new Domain.Entities.Quote
            {
                Content = command.Content
            };

            await AssignMovieToTheQuote(quote);
            await AssignCharacterToTheQuote(quote);

            _quoteRepository.Add(quote);
            await _quoteRepository.SaveChangesAsync();
            result.SetSuccess(true);
        }
        catch (Exception exception)
        {
            result.Message = exception.Message;
            _logger.LogInformation(exception, exception.Message);
        }

        return result;
    }

    private async Task AssignCharacterToTheQuote(Domain.Entities.Quote quote)
    {
        var characterByQuoteContent = await _characterByQuoteContentRepository.GetMovieByQuoteContent(quote.Content);
        var characters = characterByQuoteContent as Character[] ?? characterByQuoteContent.ToArray();
        if (!characters.Any()) throw new UserFriendlyException(ErrorMessage.NoCharacterFound);

        quote.Character = characters.FirstOrDefault()!;
    }

    private async Task AssignMovieToTheQuote(Domain.Entities.Quote quote)
    {
        var movieByQuoteContent = await _movieByQuoteContentRepository.GetMovieByQuoteContent(quote.Content);
        var movies = movieByQuoteContent.ToList();
        if (!movies.Any()) throw new UserFriendlyException(ErrorMessage.NoMovieFound);
        quote.Movie = movies.FirstOrDefault()!;
    }
}