using DocuWare.Domain.Entities;

namespace DocuWare.Infrastructure;

public static class SeedData
{
    public static void Initialize(QuoteDbContext context)
    {
        var isDbCreated = context.Database.EnsureCreated();
        if (isDbCreated)
            if (!context.Movies.Any())
                SeedMovies(context);
    }

    private static void SeedMovies(QuoteDbContext context)
    {
        var movies = new List<Movie>
        {
            new()
            {
                Title = "Gladiator",
                Quotes = new List<Quote>
                {
                    new()
                    {
                        Content = "Action, Adventure, Drama",
                        Character = new Character
                        {
                            Name = "Hero"
                        },
                        Actor = new Actor
                        {
                            Name = "Russell Crowe"
                        }
                    }
                }
            },
            new()
            {
                Title = "Gladiator1",
                Quotes = new List<Quote>
                {
                    new()
                    {
                        Content = "Action, Adventure, Drama",
                        Character = new Character
                        {
                            Name = "Hero"
                        },
                        Actor = new Actor
                        {
                            Name = "Russell Crowe"
                        }
                    }
                }
            },
            new()
            {
                Title = "Gladiator2",
                Quotes = new List<Quote>
                {
                    new()
                    {
                        Content = "Action, Adventure, Drama",
                        Character = new Character
                        {
                            Name = "Hero"
                        },
                        Actor = new Actor
                        {
                            Name = "Russell Crowe"
                        }
                    }
                }
            }
        };


        context.Movies.AddRange(movies);
        context.SaveChanges();
    }
}