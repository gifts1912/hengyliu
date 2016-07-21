using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TableOnboarding.Startup))]
namespace TableOnboarding
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
