using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppTemplate.Startup))]
namespace AppTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
