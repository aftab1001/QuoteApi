using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuWare.Domain.Entities;
using DocuWare.Infrastructure;
using DocuWare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DocuWare.UnitTest.Infrastructure;

[TestFixture]
public class CharacterByQuoteContentRepositoryTest
{
    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<QuoteDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new QuoteDbContext(_options);
        systemUnderTest = new CharacterByQuoteContentRepository(_context);
    }

    private CharacterByQuoteContentRepository systemUnderTest;
    private QuoteDbContext _context;
    private DbContextOptions<QuoteDbContext> _options;

    private IEnumerable<Character> GetCharacterWithMatchingContent(string content)
    {
        var quotes = new List<Quote>
        {
            new() {Content = content},
            new() {Content = "other content"}
        };

        return new List<Character>
        {
            new() {Quotes = quotes, Name = "character1"},
            new() {Name = "character2", Quotes = new List<Quote> {new() {Content = "another content"}}}
        };
    }


    [Test]
    public async Task GetCharacterByQuoteContent_WhenInvokedWithMatchingContent_ReturnsMatchingCharacter()
    {
        var content = await SetupCharacterWithMatchingQuoteContent();


        var result = await systemUnderTest.GetCharacterByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(content, result.First().Quotes.First().Content);
    }

    private async Task<string> SetupCharacterWithMatchingQuoteContent()
    {
        var content = "test content";
        var characters = GetCharacterWithMatchingContent(content);
        await _context.Characters.AddRangeAsync(characters);
        await _context.SaveChangesAsync();
        return content;
    }

    [Test]
    public async Task GetCharacterByQuoteContent_WhenInvokedWithNoMatchingContent_ReturnsEmptyList()
    {
        var content = await SetupCharacterByQuoteWithNonMatchingContent();

        var result = await systemUnderTest.GetCharacterByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    private async Task<string> SetupCharacterByQuoteWithNonMatchingContent()
    {
        const string content = "test content";
        var all = from c in _context.Characters select c;
        _context.Characters.RemoveRange(all);
        await _context.SaveChangesAsync();

        return content;
    }
}