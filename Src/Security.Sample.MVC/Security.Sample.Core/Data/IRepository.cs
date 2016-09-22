using System;
using System.Linq;
using System.Linq.Expressions;

namespace Security.Sample.Core.Data
{
    public interface IRepository<T>
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        T Insert(T o);
        void Save();
        void Delete(T o);
        void Restore(T o);
        void Refresh(T instance);
    }
}