using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Security.Sample.Core.Security;

namespace Security.Sample.Service.Security
{
    public class FormAuthService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie, IEnumerable<string> roles)
        {
            var str = string.Join(",", roles);

            var authTicket = new FormsAuthenticationTicket(
                1,
                userName,  
                DateTime.Now,
                DateTime.Now.AddDays(30), 
                createPersistentCookie,
                str,
                "/");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            if (authTicket.IsPersistent)
            {
                cookie.Expires = authTicket.Expiration;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
