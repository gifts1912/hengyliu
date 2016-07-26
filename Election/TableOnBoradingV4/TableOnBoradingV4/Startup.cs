using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TableOnBoradingV4.Startup))]
namespace TableOnBoradingV4
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
