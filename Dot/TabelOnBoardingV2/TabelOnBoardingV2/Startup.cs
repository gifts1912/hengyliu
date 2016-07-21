using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TabelOnBoardingV2.Startup))]
namespace TabelOnBoardingV2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
