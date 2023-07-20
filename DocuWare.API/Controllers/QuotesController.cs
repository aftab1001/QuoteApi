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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteCommand command)
    {
        _logger.LogInformation(SendCreateQuoteCommandRequest);
        var result = await _mediator.Send(command);

        if (result.Success) return Ok("Successfully created quote");

        return BadRequest(result.Message);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuotes()
    {
        var query = new GetQuotesQuery();
        var quotes = await _mediator.Send(query);

        if (quotes.Success) return Ok(quotes);

        return BadRequest(quotes.Message);
    }

    [HttpGet("by-movie/{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuotesByMovie(int movieId)
    {
        var query = new GetQuotesByMovieQuery(movieId);
        var quotes = await _mediator.Send(query);

        if (quotes.Success) return Ok(quotes);

        return BadRequest(quotes.Message);
    }

    [HttpGet("by-actor/{actorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuotesByActor(int actorId)
    {
        var query = new GetQuotesByActorQuery(actorId);
        var quotes = await _mediator.Send(query);

        if (quotes.Success) return Ok(quotes);

        return BadRequest(quotes.Message);
    }

    [HttpPost("quotes/{quoteId}/assign-actor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignActorToQuote(int quoteId, int actorId)
    {
        var command = new AssignActorToQuoteCommand {QuoteId = quoteId, ActorId = actorId};
        var result = await _mediator.Send(command);

        if (result.Success) return Ok("Actor successfully assigned to the quote.");

        return BadRequest(result.Message);
    }
}