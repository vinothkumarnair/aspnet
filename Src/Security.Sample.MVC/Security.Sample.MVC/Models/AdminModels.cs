using PagedList;
using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Security.Sample.MVC.Models
{
    public class UserListModels
    {
        public UserListModels()
        {
            PageNumber = 1;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Role> RoleList { get; set; }
        public int? SelectedRoleId { get; set; }
        public int PageNumber { get; set; }
        public IPagedList<User> Users { get; set; }

    }

    public class UserModels
    {
        public User User { get; set; }

        //[Required(ErrorMessage = "Select the Role.")]
        public int? SelectedRoleId { get; set; }

        public List<Role> RoleList { get; set; }

    }


    public class UserEditModels
    {

        public int Id { get; set; }
        public bool UserStatus { get; set; }
        public string Password { get; set; }
        //[Required(ErrorMessage = "Select the Role.")]
        public int? SelectedRoleId { get; set; }

        public List<Role> RoleList { get; set; }

    }




}