using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Security.Sample.MVC.Controllers
{
    public class UserContext : IPrincipal
    {
           private IPrincipal _principal;
           public string DeviceID { get; set; }
           public string UserName { get; set; }
           public string RoleName { get; set; }
           public string UserId { get; set; }
           public string RoleId { get; set; }
           public User UserEntity { get; set; }

        public IIdentity Identity
        {
            get { return _principal.Identity; }
        }

        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }
        public void SetPrincipal(IPrincipal principal)
        {
            _principal = principal;
        }
    }
}
