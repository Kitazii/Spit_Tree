using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(K_Burns_SpitTree.Startup))]
namespace K_Burns_SpitTree
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
