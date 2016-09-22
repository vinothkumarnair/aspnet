using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security.Sample.Service.Security;
using Security.Sample.Core.Model;
using Common.Logging;
using Security.Sample.Core.Data;
using Security.Sample.Core;
using Security.Sample.Core.Services;

namespace Security.Sample.Service
{
    public class UserAccountService : ServiceBase, IUserAccountService
    {
        private readonly IStringHasher _hasher;
        private readonly ILog _log = LogManager.GetLogger("Security.Sample.Service");
        private IUnitOfWork _unitOfWork;


        public UserAccountService()
        {

            _hasher = new StringHasher();
            _hasher.SaltSize = 10;
            _unitOfWork = UnitOfWork;

        }

        public Role GetRole(string rolename)
        {
            Role role = null;
            try
            {
                role = _unitOfWork.UserAccountRepository.GetAllRoles().Where(o => o.Name == rolename).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _log.Error("Getting user information failed for Role .", ex);
                throw ex;
            }
            return role;

        }

        public void DeleteUser(int id)
        {
            User user = _unitOfWork.UserAccountRepository.GetUserById(id);
            _unitOfWork.UserAccountRepository.DeleteUser(user);
            _unitOfWork.SaveChanges();
        }

        public int Create(User user)
        {
            try
            {
                if (IsUnique(user.Login))
                {
                    string planTextPassword = user.Password;
                    user.Password = _hasher.Encrypt(user.Password);
                    User createdUser = _unitOfWork.UserAccountRepository.CreateUser(user);
                    _unitOfWork.SaveChanges();

                    return createdUser.Id;
                }
                else
                {
                    throw new Exception("User already exist. Please try with some other user name.");
                }
            }
            catch (Exception ex)
            {
                _log.Error("User Creation failed.", ex);
                throw ex;
            }
            return 0;
        }

        public int UpdateUser(User user)
        {
            try
            {
                _unitOfWork.UserAccountRepository.UpdateUser(user);
                _unitOfWork.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                _log.Error("User Creation failed.", ex);
                throw ex;
            }
            return 0;
        }

        public bool IsUnique(string login)
        {
            return _unitOfWork.UserAccountRepository.Where(o => o.Login == login).Count() == 0;
        }

        public User Get(string login, string password)
        {
            try
            {
                var user = _unitOfWork.UserAccountRepository.Where(o => o.Login == login && o.UserStatus == true).SingleOrDefault();
                if (user == null || !_hasher.CompareStringToHash(password, user.Password)) return null;
                return user;
            }
            catch (Exception ex)
            {
                _log.Error("Getting user information failed for user ." + login, ex);
                throw ex;
            }
            return null;
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.UserAccountRepository.GetUserById(id);
        }


        public User GetUserByEmail(string emailId)
        {
            return _unitOfWork.UserAccountRepository.GetUserByEmail(emailId);
        }


        public int GetId(string login)
        {
            try
            {
                var user = _unitOfWork.UserAccountRepository.Where(o => o.Login == login).SingleOrDefault();
                return user.Id;
            }
            catch (Exception ex)
            {
                _log.Error("Getting user id failed for user ." + login, ex);
                throw ex;
            }
            return 0;
        }

        public User GetUserByName(string login)
        {
            try
            {
                var user = _unitOfWork.UserAccountRepository.Where(o => o.Login == login).SingleOrDefault();
                if (user == null) return null;
                return user;
            }
            catch (Exception ex)
            {
                _log.Error("Getting user information failed for user ." + login, ex);
                throw ex;
            }
            return null;
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            try
            {
                var user = _unitOfWork.UserAccountRepository.GetUserById(id);
                if (user != null && _hasher.CompareStringToHash(oldPassword, user.Password))
                {
                    _unitOfWork.UserAccountRepository.GetUserById(id).Password = _hasher.Encrypt(newPassword);
                    _unitOfWork.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("User id not found.");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Getting user id failed for userid ." + id, ex);
            }
            return false;
        }

        public bool ChangePassword(int id, string newPassword)
        {
            try
            {
                _unitOfWork.UserAccountRepository.GetUserById(id).Password = _hasher.Encrypt(newPassword);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Getting user id failed for userid ." + id, ex);
            }
            return false;
        }


        public List<User> GetAllUsers()
        {
            return _unitOfWork.UserAccountRepository.GetAllUsers().ToList();
        }


        public List<Role> GetRoles()
        {
            return _unitOfWork.UserAccountRepository.GetAllRoles().ToList();
        }

        public User ValidateHintAnswer(int userid, string hintAnswer)
        {
            User user = _unitOfWork.UserAccountRepository.GetUserById(userid);
            if (user != null && user.HintAnswer == hintAnswer)
                return user;
            else
                return null;

        }

    }
}
