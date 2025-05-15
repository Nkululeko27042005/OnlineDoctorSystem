using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoctorSystem.Startup))]
namespace DoctorSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
