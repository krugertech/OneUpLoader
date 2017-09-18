using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneUpLoader.Startup))]
namespace OneUpLoader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
