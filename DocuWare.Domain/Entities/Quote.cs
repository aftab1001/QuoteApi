namespace DocuWare.Domain.Entities;

public class Quote
{
    public int Id { get; set; }
    public string Content { get; set; }

    public ICollection<Movie> Movies { get; set; }

    public ICollection<Character> Characters { get; set; }
}