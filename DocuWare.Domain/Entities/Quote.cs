namespace DocuWare.Domain.Entities;

public class Quote
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }

    public int CharacterId { get; set; }
    public virtual Character Character { get; set; }

    public int ActorId { get; set; }
    public virtual Actor Actor { get; set; }
}