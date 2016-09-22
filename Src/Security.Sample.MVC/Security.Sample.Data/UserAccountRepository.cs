using Security.Sample.Core.Model;
using Security.Sample.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Omu.ValueInjecter;
using Security.Sample.Core.Data;

namespace Security.Sample.Data
{
    public class UserAccountRepository : IUserAccountRepository
    {

        protected readonly DbContext dbContext;

        public UserAccountRepository(DbContext context)
        {
            dbContext = context;
        }

        public User CreateUser(User o)
        {
            var t = dbContext.Set<User>().Create();
            t.InjectFrom(o);
            dbContext.Set<User>().Add(t);
            return t;
        }


        public void UpdateUser(User obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;

        }

        public void Refresh(User instance)
        {
            dbContext.Entry<User>(instance).Reload();
        }

        public virtual void DeleteUser(User o)
        {
            dbContext.Set<User>().Remove(o);
        }

        public User GetUserById(int id)
        {
            return dbContext.Set<User>().Where(i => i.Id == id).FirstOrDefault();
        }

        public User GetUserByEmail(string  email)
        {
            return dbContext.Set<User>().Where(i => i.Email == email).FirstOrDefault();
        }

        public virtual IQueryable<User> Where(Expression<Func<User, bool>> predicate, bool showDeleted = false)
        {
            return dbContext.Set<User>().Where(predicate);
        }

        public virtual IQueryable<User> GetAllUsers()
        {
            return dbContext.Set<User>();
        }



        public virtual void DeleteAllUsers()
        {
            dbContext.Set<User>().RemoveRange((IEnumerable<User>)dbContext.Set<User>());
        }

        public virtual IQueryable<User> GetAllUserWithNoTracking()
        {
            return dbContext.Set<User>().AsNoTracking();
        }

        public virtual IQueryable<Role> GetAllRoles()
        {
            return dbContext.Set<Role>().Include(i => i.ReportingTo);
        }

        


    }
}
