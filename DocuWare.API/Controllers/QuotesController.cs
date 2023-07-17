using DocuWare.Application.Features.Quote.Command;
using DocuWare.Application.Features.Quote.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocuWare.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotesController : ControllerBase
{
    private const string SendCreateQuoteCommandRequest = "Sending create quote command request";
    private readonly ILogger<QuotesController> _logger;
    private readonly IMediator _mediator;

    public QuotesController(ILogger<QuotesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteCommand command)
    {
        _logger.LogInformation(SendCreateQuoteCommandRequest);
        _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetQuotes()
    {
        // Create the query and send it to the mediator
        var query = new GetQuotesQuery();
        var quotes = _mediator.Send(query);

        return Ok(quotes);
    }

    [HttpGet("by-movie/{movieId}")]
    public IActionResult GetQuotesByMovie(int movieId)
    {
        var query = new GetQuotesByMovieQuery(movieId);
        var quotes = _mediator.Send(query);

        return Ok(quotes);
    }

    [HttpGet("by-actor/{actorId}")]
    public IActionResult GetQuotesByActor(int actorId)
    {
        var query = new GetQuotesByActorQuery(actorId);
        var quotes = _mediator.Send(query);

        return Ok(quotes);
    }

    [HttpPost("quotes/{quoteId}/assign-actor")]
    public async Task<IActionResult> AssignActorToQuote(int quoteId, int actorId)
    {
        try
        {
            var command = new AssignActorToQuoteCommand {QuoteId = quoteId, ActorId = actorId};
            await _mediator.Send(command);

            return Ok("Actor successfully assigned to the quote.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while assigning the actor to the quote.");
        }
    }
}