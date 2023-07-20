using System;
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
public class RepositoryTests
{
    private IRepository<Character> _repository;
    private QuoteDbContext _dbContext;
    private DbSet<Character> _dbSet;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<QuoteDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new QuoteDbContext(options);
        _dbSet = _dbContext.Set<Character>();

        _repository = new Repository<Character>(_dbContext);
    }


    [Test]
    public async Task Add_Entity_ShouldBeAdded()
    {
        var entityToAdd = new Character
        {
            Name = "TestCharacter"
        };

        _repository.Add(entityToAdd);
        await _dbContext.SaveChangesAsync();

        Assert.AreEqual(1, _dbSet.Count());
    }

    [Test]
    public async Task Update_Entity_ShouldBeAttachedAndModified()
    {
        var entityToUpdate = new Character
        {
            Name = "TestCharacter"
        };
        _dbSet.Add(entityToUpdate);
        await _dbContext.SaveChangesAsync();

        _repository.Update(entityToUpdate);

        Assert.AreEqual(EntityState.Modified, _dbContext.Entry(entityToUpdate).State);
    }


    [Test]
    public async Task GetByIdAsync_ValidId_ShouldReturnEntity()
    {
        var id = 1;
        var entity = new Character {Id = id, Name = "TestCharacter"};
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(id);

        Assert.AreEqual(entity, result);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var entities = new List<Character>
        {
            new() {Id = 1, Name = "TestCharacter"},
            new() {Id = 2, Name = "TestCharacter"},
            new() {Id = 3, Name = "TestCharacter"}
        };
        _dbSet.AddRange(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(entities.Count, result.Count());
    }

    [Test]
    public async Task GetById_ValidId_ShouldReturnEntity()
    {
        // Arrange
        var id = 1;
        var entity = new Character {Id = id, Name = "TestCharacter"};
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(id);

        // Assert
        Assert.AreEqual(entity, result);
    }

    [Test]
    public async Task GetAll_ShouldReturnAllEntities()
    {
        // Arrange
        var entities = new List<Character>
        {
            new() {Id = 1, Name = "TestCharacter"},
            new() {Id = 2, Name = "TestCharacter"},
            new() {Id = 3, Name = "TestCharacter"}
        };
        _dbSet.AddRange(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(entities.Count, result.Count());
    }
}