namespace DocuWare.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Quote> Quotes { get; set; }

    public ICollection<Character> Characters { get; set; }
}