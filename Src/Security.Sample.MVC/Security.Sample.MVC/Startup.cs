using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Security.Sample.MVC.Startup))]
namespace Security.Sample.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
