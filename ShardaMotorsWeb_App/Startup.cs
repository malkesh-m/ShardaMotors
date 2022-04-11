using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShardaMotorsWeb_App.Startup))]
namespace ShardaMotorsWeb_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
