using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeetClinic_MVC.Startup))]
namespace FeetClinic_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
