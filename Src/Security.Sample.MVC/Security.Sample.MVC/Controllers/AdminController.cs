using Security.Sample.Core.Model;
using Security.Sample.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Security.Sample.Core.Services;

namespace Security.Sample.MVC.Controllers
{
     
    public class AdminController : BaseController
    {
        private IUserAccountService _userAccountService;
        private IMenuService _menuService;
        public AdminController()
        {
            _userAccountService = AccountService;
            _menuService = MenuService;
        }

        //
        // GET: /Admin/
        public IEnumerable<Menu> getMenus()
        {
            return _menuService.GetMenuForGivenRole(Convert.ToInt32(UserContext.RoleId));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManagement(UserListModels model)
        {
            
            List<User> users = null;

            model.RoleList = _userAccountService.GetRoles();

            users = _userAccountService.GetAllUsers().Where(i=>i.Login != "admin").ToList();

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                users = users.Where(i => i.FirstName.Contains(model.FirstName)).ToList();

            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                users = users.Where(i => i.FirstName.Contains(model.LastName)).ToList();
            }
            if (model.SelectedRoleId != null)
            {
                Role selectedRole = model.RoleList.Where(i => i.Id == model.SelectedRoleId).FirstOrDefault();
                users = users.Where(i => i.Roles.Contains(selectedRole)).ToList();
            }

            if (users != null)
                model.Users = users.ToPagedList(model.PageNumber, 15);

            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            

            User user = _userAccountService.GetUserById((int)id);
            
            UserEditModels model = new UserEditModels();
            model.Id = user.Id;
            model.UserStatus = user.UserStatus;

            model.SelectedRoleId = user.Roles.FirstOrDefault().Id;

            model.RoleList = _userAccountService.GetRoles();

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            string message="";
            try
            {
                _userAccountService.DeleteUser((int)id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return RedirectToAction("UserManagement", new { Message = message });
        }

        [HttpPost]
        
        public ActionResult Edit(UserEditModels userModel)
        {
            
            User userFromDB = _userAccountService.GetUserById(userModel.Id);
            if (ModelState.IsValid)
            {
             
                userFromDB.UserStatus = userModel.UserStatus;

                userFromDB.Password = userModel.Password!=null ? userModel.Password : userFromDB.Password;
                userFromDB.Roles.Clear();

                Role selectedRole = _userAccountService.GetRoles().Where(i => i.Id == userModel.SelectedRoleId).FirstOrDefault();
                List<Role> roles = new List<Role>();
                roles.Add(selectedRole);
                userFromDB.Roles = roles;

                _userAccountService.UpdateUser(userFromDB);
                return RedirectToAction("UserManagement");
            }
            else
            {
                userModel.RoleList = _userAccountService.GetRoles();
                return View(userModel);
            }
           
        }



    }
}
