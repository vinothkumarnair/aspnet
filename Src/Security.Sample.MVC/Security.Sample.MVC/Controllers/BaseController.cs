using Security.Sample.Core.Data;
using Security.Sample.Core.Model;
using Security.Sample.Core.Services;
using Security.Sample.Data;
using Security.Sample.Data.DBContext;
using Security.Sample.Service;
using Security.Sample.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Security.Sample.MVC.Controllers
{
  
    public class BaseController  : Controller
    {
        
        private UserContext _userContext;
        private IUserAccountService _accountService;
        private IMenuService _menuService;
        public IUnitOfWork _unitOfWork;

        public IUserAccountService AccountService { get { return _accountService; } }

        public IMenuService MenuService { get { return _menuService; } }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        public BaseController()
        {
            IDbContextFactory dbContextFactory = new DbContextFactory();
            _unitOfWork = new UnitOfWork(dbContextFactory);
            _accountService = new UserAccountService();
            _userContext = new UserContext();
            _menuService = new MenuService();
        }
        public IEnumerable<Menu> GetMenus()
        {
            return _menuService.GetMenuForGivenRole(Convert.ToInt32(UserContext.RoleId));
        }


        public UserContext UserContext
        {
            get { return _userContext; }
            set { _userContext = value; }
        }

        protected override void Initialize(System.Web.Routing.RequestContext context)
        {
            base.Initialize(context);
            HttpContextBase httpContext = context.HttpContext;
            httpContext.Session["dummy"] = String.Empty;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                _userContext = new UserContext();
                _userContext.DeviceID = httpContext.Request.UserHostAddress;
                _userContext.UserName = httpContext.User.Identity.Name;
                _userContext.UserId = _accountService.GetUserByName(httpContext.User.Identity.Name).Id.ToString();
                _userContext.RoleName = _accountService.GetUserByName(httpContext.User.Identity.Name).Roles.FirstOrDefault().Name;
                _userContext.RoleId = _accountService.GetUserByName(httpContext.User.Identity.Name).Roles.FirstOrDefault().Id.ToString();
                _userContext.UserEntity = _accountService.GetUserByName(httpContext.User.Identity.Name);
                var identity = new GenericIdentity(_userContext.UserName, "Forms");
                string[] roles = new string[] { _userContext.RoleName };
                var principal = new GenericPrincipal(identity, roles);
                
                _userContext.SetPrincipal(principal);
                httpContext.User = principal;
                Thread.CurrentPrincipal = _userContext;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Menus = GetMenus(); 
            base.OnActionExecuting(filterContext);
        }

    }
}