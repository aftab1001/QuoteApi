using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using DocuWare.Infrastructure;
using DocuWare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DocuWare.UnitTest.Infrastructure;

[TestFixture]
public class ActorByQuoteContentRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<QuoteDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new QuoteDbContext(_options);
        systemUnderTest = new ActorByQuoteContentRepository(_context);
    }

    private IActorByQuoteContentRepository systemUnderTest;
    private QuoteDbContext _context;
    private DbContextOptions<QuoteDbContext> _options;


    private IEnumerable<Actor> GetActorsWithMatchingContent(string content)
    {
        var quotes = new List<Quote>
        {
            new() {Content = content},
            new() {Content = "other content"}
        };

        return new List<Actor>
        {
            new() {Quotes = quotes, Name = "Actor1"},
            new() {Name = "Actor2", Quotes = new List<Quote> {new() {Content = "another content"}}}
        };
    }


    [Test]
    public async Task GetActorByQuoteContent_WithMatchingContent_ReturnsMatchingActors()
    {
        var content = await SetupActorWithMatchingQuoteContent();


        var result = await systemUnderTest.GetActorByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(content, result.First().Quotes.First().Content);
    }

    private async Task<string> SetupActorWithMatchingQuoteContent()
    {
        var content = "test content";
        var actors = GetActorsWithMatchingContent(content);
        await _context.Actors.AddRangeAsync(actors);
        await _context.SaveChangesAsync();
        return content;
    }

    [Test]
    public async Task GetActorByQuoteContent_WithNoMatchingContent_ReturnsEmptyList()
    {
        var content = await SetupActorByQuoteWithNonMatchingContent();

        var result = await systemUnderTest.GetActorByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    private async Task<string> SetupActorByQuoteWithNonMatchingContent()
    {
        var content = "test content";
        var all = from c in _context.Actors select c;
        _context.Actors.RemoveRange(all);
        await _context.SaveChangesAsync();
        return content;
    }
}