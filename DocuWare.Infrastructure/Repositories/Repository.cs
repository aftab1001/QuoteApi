using DocuWare.Application.Contracts;
using DocuWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly QuoteDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(QuoteDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _dbSet.FindAsync(id))!;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


    public Task<IEnumerable<Quote>> GetQuotesByActorAsync(int actorId)
    {
        return Task.FromResult<IEnumerable<Quote>>(_dbSet.OfType<Quote>().Include(x => x.Characters)
            .ThenInclude(y => y.Actor)
            .Where(q => q.Characters.Any(x => x.Actor.Id == actorId))
            .ToList());
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id)!;
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }
}