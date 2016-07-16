using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TableOnBoardingV2.Startup))]
namespace TableOnBoardingV2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
