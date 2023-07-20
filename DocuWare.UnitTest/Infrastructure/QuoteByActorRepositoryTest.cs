using System.Linq;
using System.Threading.Tasks;
using DocuWare.Domain.Entities;
using DocuWare.Infrastructure;
using DocuWare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DocuWare.UnitTest.Infrastructure;

[TestFixture]
public class QuoteByActorRepositoryTest
{
    private QuoteByActorRepository systemUnderTest;
    private QuoteDbContext _context;
    private DbContextOptions<QuoteDbContext> _options;


    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<QuoteDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new QuoteDbContext(_options);
        systemUnderTest = new QuoteByActorRepository(_context);
    }

    [Test]
    public async Task GetMovieByQuoteContent_WhenInvoked_ReturnsMatchingQuote()
    {
        var entityId = await SetupQuotesByActor();
        var result = await systemUnderTest.GetQuotesByActorAsync(entityId);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
    }

    private async Task<int> SetupQuotesByActor()
    {
        var result = await _context.Quotes.AddAsync(new Quote
        {
            Content = "TestQuote",
            Actor = new Actor
            {
                Name = "TestActor"
            },
            Character = new Character
            {
                Name = "test char"
            },
            Movie = new Movie
            {
                Title = "testmovie"
            }
        });
        await _context.SaveChangesAsync();
        return result.Entity.ActorId;
    }
}