using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Security.Sample.Core.Data;
using Security.Sample.Core.Services;
namespace Security.Sample.MVC.Controllers
{
     [Authorize]
    public class MenuController : BaseController
    {
        private IMenuService _menuService;

        public MenuController()
        {
            _menuService = MenuService;
        }

        public ActionResult Index(int page=1)
        {
            int currentUserRole= Convert.ToInt32(UserContext.RoleId);
            IEnumerable<Menu> model = _menuService.GetMenuForGivenRole(currentUserRole);
            return View(model.ToPagedList(page, 10));
        }
       

    }
}
