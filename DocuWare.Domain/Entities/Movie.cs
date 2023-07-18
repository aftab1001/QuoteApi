namespace DocuWare.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; }
}