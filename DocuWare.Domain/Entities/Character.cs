namespace DocuWare.Domain.Entities;

public class Character
{
    public int Id { get; set; }

    public int ActorId { get; set; }
    public string Name { get; set; }
    public ICollection<Quote> Quotes { get; set; }
    public Actor Actor { get; set; }
}