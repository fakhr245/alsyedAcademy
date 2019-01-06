using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(alsyedAcademy.Startup))]
namespace alsyedAcademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
