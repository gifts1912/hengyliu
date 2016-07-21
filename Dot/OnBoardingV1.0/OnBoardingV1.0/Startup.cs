using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnBoardingV1._0.Startup))]
namespace OnBoardingV1._0
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
