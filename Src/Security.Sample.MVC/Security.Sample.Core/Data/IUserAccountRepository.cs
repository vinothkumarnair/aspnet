using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Security.Sample.Core.Data
{
    public interface IUserAccountRepository
    {

        User CreateUser(User o);

        void DeleteUser(User o);

        User GetUserById(int id);

        void Refresh(User instance);

        IQueryable<User> Where(Expression<Func<User, bool>> predicate, bool showDeleted = false);

        IQueryable<User> GetAllUsers();


        void DeleteAllUsers();

        IQueryable<User> GetAllUserWithNoTracking();

        IQueryable<Role> GetAllRoles();

        void UpdateUser(User obj);

        User GetUserByEmail(string email);


    }
}
