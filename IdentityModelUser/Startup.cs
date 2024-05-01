using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityModelUser.Startup))]
namespace IdentityModelUser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
