using DocuWare.Domain.Entities;

namespace DocuWare.Infrastructure;

public static class SeedData
{
    public static void Initialize(QuoteDbContext context)
    {
        if (!context.Movies.Any()) SeedMovies(context);
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
                        Content = "Action, Adventure, Drama"
                    }
                },
                Characters = new List<Character>
                {
                    new()
                    {
                        Name = "Hero",
                        Actor = new Actor
                        {
                            Name = "Russell Crowe"
                        },
                        Quotes = new List<Quote>
                        {
                            new()
                            {
                                Content = "Action, Adventure, Drama"
                            }
                        }
                    }
                }
            },
            new()
            {
                Title = "Primal Fear",
                Quotes = new List<Quote>
                {
                    new()
                    {
                        Content = "Drama"
                    }
                },
                Characters = new List<Character>
                {
                    new()
                    {
                        Name = "Hero",
                        Actor = new Actor
                        {
                            Name = "Richard Gere"
                        },
                        Quotes = new List<Quote>
                        {
                            new()
                            {
                                Content = "Action, Adventure, Drama"
                            }
                        }
                    }
                }
            },
            new()
            {
                Title = "The Godfather",
                Quotes = new List<Quote>
                {
                    new()
                    {
                        Content = "Adventure"
                    }
                },
                Characters = new List<Character>
                {
                    new()
                    {
                        Name = "Hero",
                        Actor = new Actor
                        {
                            Name = "Vincent Cassel"
                        },
                        Quotes = new List<Quote>
                        {
                            new()
                            {
                                Content = "Action, Adventure, Drama"
                            }
                        }
                    }
                }
            }
        };

        context.Movies.AddRange(movies);
        context.SaveChanges();
    }
}