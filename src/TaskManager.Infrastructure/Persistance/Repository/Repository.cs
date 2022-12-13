using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application;

namespace TaskManager.Infrastructure.Persistance.Repository;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> dbSet;

    private readonly TaskManagerDbContext _dbContext;

    public Repository(TaskManagerDbContext context)
    {
        this._dbContext = context;
        this.dbSet = context.Set<T>();
    }

    public void Add(T entity)
    {
        this.dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        this.dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        this.dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        this.dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        this.dbSet.RemoveRange(entities);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await this.dbSet.ToListAsync();
    }

    public async Task<T?> GetById(Guid Id)
    {
        return await dbSet.FindAsync(Id);
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Expression<Func<T, object>> include = null, int? skip = null, int? take = null)
    {
        IQueryable<T> query = this.dbSet;

        if (include is not null)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    // public async Task<IEnumerable<T>> Find(ISpecification<T> specification, CancellationToken cancellationToken)
    // {
    //     return await this.dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);
    // }
    //
    // public async Task<bool> Exists(ISpecification<T> specification, CancellationToken cancellationToken)
    // {
    //     return await this.dbSet.Where(specification.ToExpression()).AnyAsync(cancellationToken);
    // }
    //
    // public async Task<IEnumerable<TType>> Find<TType>(ISpecification<T> specification, Expression<Func<T, TType>> selector, CancellationToken cancellationToken)
    // {
    //     return await this.dbSet.Where(specification.ToExpression()).Select(selector).ToListAsync(cancellationToken);
    // }
}