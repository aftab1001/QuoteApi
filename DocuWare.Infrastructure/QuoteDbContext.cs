using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure;

public class QuoteDbContext : DbContext
{
    public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Character> Characters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure any additional mappings or constraints here
    }
}