using System;
using System.Linq;
using System.Web.Mvc;
using Security.Sample.Core.Security;
using Security.Sample.Core.Model;
using Security.Sample.Core.Services;
using Security.Sample.MVC.Models;
using Security.Sample.Service.Security;
using System.Collections.Generic;

namespace Security.Sample.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IFormsAuthentication _formsAuth;
        private readonly IUserAccountService _accountService;

        private IMenuService _menuService;
        public AccountController()
        {
            _formsAuth = new FormAuthService();
            _accountService = AccountService;
            _menuService = MenuService;

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string message)
        {
            ViewBag.InformationMessage = message;
            if(string.IsNullOrEmpty(message))
                ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
       
        
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {

                User user;
                try
                {
                    user = _accountService.Get(model.UserName, model.Password);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Some proplem in validating your login. Please try again later.");
                    return View();
                }

                if (user == null)
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View();
                }


                _formsAuth.SignIn(user.Login, model.RememberMe, user.Roles.Select(o => o.Name));
                HttpContext.Session["dummy"] = String.Empty;

                if (returnUrl != null ) 
                {
                    return Redirect(returnUrl);
                }
                else
                {

                    return Redirect("/Menu/Index");

                }


            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        public ActionResult LogOff()
        {
            _formsAuth.SignOut();
            return RedirectToAction("Login", "Account");
        }


        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {

            ViewBag.InformationMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            if (User.Identity.Name != null)
                ViewBag.HasLocalPassword = true;

            ViewBag.ReturnUrl = Url.Action("Manage");

            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
       
        [Authorize]
        public ActionResult Manage(LocalPasswordModel model)
        {

            ViewBag.HasLocalPassword = true;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = _accountService.ChangePassword(_accountService.GetId(User.Identity.Name), model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    _formsAuth.SignOut();
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                else
                {
                    ViewBag.ErrorMessage = "Last Operation failed. Please retry with valid data.";
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {

            UserModels userModels = new UserModels();

            userModels.RoleList = _accountService.GetRoles();

            return View(userModels);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult Create(UserModels userModel)
        {

            Role selectedRole = _accountService.GetRoles().Where(i => i.Name == "Anonymous").FirstOrDefault();
            if (ModelState.IsValid)
            {

                List<Role> roles = new List<Role>();
                roles.Add(selectedRole);
                userModel.User.Roles = roles;

            }
            else
            {
                userModel.RoleList = _accountService.GetRoles();
                return View(userModel);
            }

            try
            {
                userModel.User.UserStatus = true;
                _accountService.Create(userModel.User);
                return RedirectToAction("Login", "Account", new { Message = "Registration Successful. Please login..." });
            }
            catch (Exception ex)
            {
                userModel.RoleList = _accountService.GetRoles();

                ModelState.AddModelError("User", ex.Message);
                return View(userModel);
            }


        }


        [AllowAnonymous]
        public ActionResult ForgotPassword(string message)
        {
            ForgotPasswordModels model = new ForgotPasswordModels();
            ViewBag.ErrorMessage = message;
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPasswordEmail(ForgotPasswordModels model)
        {
            User user= _accountService.GetUserByEmail(model.EmailValidation.Email);
            if (user != null)
            {
                ForgotPasswordHintModels hintModel = new ForgotPasswordHintModels();
                model.HintAnswerValidation = hintModel;
                model.HintAnswerValidation.Id = user.Id;
                model.HintAnswerValidation.HintQuestion = user.HintQuestion;
            }
            else
            {
                return RedirectToAction("ForgotPassword", new { Message = "Email address not Found. Please try again with the valid email address." });
            }


            return View("ForgotPassword",model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPasswordHint(ForgotPasswordModels model)
        {
            User user = _accountService.ValidateHintAnswer(model.HintAnswerValidation.Id,model.HintAnswerValidation.HintAnswer);

            if (user != null)
            {
                bool changePasswordSucceeded = _accountService.ChangePassword(user.Id, model.HintAnswerValidation.NewPassword);
                if(changePasswordSucceeded)
                    return RedirectToAction("Login", "Account", new { Message = "Password updated. Please login with your new password..." });
                else
                    return RedirectToAction("ForgotPassword", new { Message = "Something went wrong. Please try again. " });
            }
            else
            {
                return RedirectToAction("ForgotPassword", new { Message = "Something went wrong with the answer. Please try again. " });
            }

            return View("ForgotPassword", model);
        }

    }
    
}