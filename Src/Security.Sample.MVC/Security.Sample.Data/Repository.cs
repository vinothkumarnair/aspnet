using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Security.Sample.Core.Model;
using Security.Sample.Data.DBContext;
using  Omu.ValueInjecter;
using Security.Sample.Core.Data;
namespace Security.Sample.Data
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DbContext dbContext;

        public Repository(DbContext context)
        {
            dbContext = context;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public T Insert(T o)
        {
            var t = dbContext.Set<T>().Create();
            t.InjectFrom(o);
            dbContext.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T o)
        {
  
                dbContext.Set<T>().Remove(o);
        }

        public T Get(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public void Restore(T o)
        {

        }
        public void Refresh(T instance)
        {
            dbContext.Entry<T>(instance).Reload();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }
    }
}
