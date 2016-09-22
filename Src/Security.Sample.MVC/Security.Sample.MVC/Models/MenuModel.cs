using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.MVC.Models
{
    public class MenuAccessModel
    {
        public Dictionary<Menu,int[]> Menus { get; set; }
        public int[] RoleIds { get; set; }
        public List<Role> Roles { get; set; }
    }

}
