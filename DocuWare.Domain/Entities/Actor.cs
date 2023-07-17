namespace DocuWare.Domain.Entities;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Character Character { get; set; }
}