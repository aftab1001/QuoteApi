namespace DocuWare.Application.Features.Quote.Dtos;

public class QuoteResponse
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int MovieId { get; set; }

    public int CharacterId { get; set; }
    public int ActorId { get; set; }
}