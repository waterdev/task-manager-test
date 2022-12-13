using System.Linq.Expressions;

namespace TaskManager.Application;

public interface IRepository<T>
{
    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
    
    Task<IEnumerable<T>> GetAll();
    
    Task<T?> GetById(Guid id);

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Expression<Func<T, object>> include = null, int? skip = null, int? take = null);

    Task SaveChanges();
}