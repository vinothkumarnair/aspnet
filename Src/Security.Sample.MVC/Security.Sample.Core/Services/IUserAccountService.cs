using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security.Sample.Core.Model;

namespace Security.Sample.Core.Services
{
    public interface IUserAccountService : IServiceBase
    {
        bool IsUnique(string login);
        bool ChangePassword(int id, string oldPassword,string newPassword);
        User Get(string Login, string password);
        int GetId(string login);
        int Create(User user);
        Role GetRole(string rolename);
        User GetUserByName(string login);
        User GetUserById(int id);
        List<User> GetAllUsers();
        List<Role> GetRoles();
        int UpdateUser(User user);
        void DeleteUser(int id);
        User GetUserByEmail(string emailId);
        User ValidateHintAnswer(int userid, string hintAnswer);
        bool ChangePassword(int id, string newPassword);
    }
}