using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(capaWeb1.Startup))]
namespace capaWeb1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
