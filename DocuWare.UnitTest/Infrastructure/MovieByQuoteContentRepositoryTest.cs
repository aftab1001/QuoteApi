using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using DocuWare.Infrastructure;
using DocuWare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DocuWare.UnitTest.Infrastructure;

[TestFixture]
public class MovieByQuoteContentRepositoryTest
{
    private IMovieByQuoteContentRepository systemUnderTest;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<QuoteDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new QuoteDbContext(_options);
        systemUnderTest = new MovieByQuoteContentRepository(_context);
    }

    private QuoteDbContext _context;
    private DbContextOptions<QuoteDbContext> _options;

    private IEnumerable<Movie> GetMovieWithMatchingContent(string content)
    {
        var quotes = new List<Quote>
        {
            new() {Content = content},
            new() {Content = "other content"}
        };

        return new List<Movie>
        {
            new() {Quotes = quotes, Title = "movie1"},
            new() {Title = "movie2", Quotes = new List<Quote> {new() {Content = "another content"}}}
        };
    }


    [Test]
    public async Task GetMovieByQuoteContent_WhenInvokedWithMatchingContent_ReturnsMatchingMovie()
    {
        var content = await SetupMovieWithMatchingQuoteContent();


        var result = await systemUnderTest.GetMovieByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(content, result.First().Quotes.First().Content);
    }

    private async Task<string> SetupMovieWithMatchingQuoteContent()
    {
        var content = "test content";
        var movies = GetMovieWithMatchingContent(content);
        await _context.Movies.AddRangeAsync(movies);
        await _context.SaveChangesAsync();
        return content;
    }

    [Test]
    public async Task GetMovieByQuoteContent_WhenInvokedWithNoMatchingContent_ReturnsEmptyList()
    {
        var content = await SetupMovieByQuoteWithNonMatchingContent();

        var result = await systemUnderTest.GetMovieByQuoteContent(content);

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    private async Task<string> SetupMovieByQuoteWithNonMatchingContent()
    {
        const string content = "test content";
        var all = from c in _context.Movies select c;
        _context.Movies.RemoveRange(all);
        await _context.SaveChangesAsync();

        return content;
    }
}