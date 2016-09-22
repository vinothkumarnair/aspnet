
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Security.Sample.Core.Security;
using Security.Sample.Core.Services;

namespace Security.Sample.MVC.Controllers
{
    public class HomeController : BaseController
    {
     
        public HomeController()
        {
           
        }

        public ActionResult Error()
        {
            

            return View();
        }


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
