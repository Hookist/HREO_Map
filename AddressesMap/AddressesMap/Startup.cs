using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddressesMap.Startup))]
namespace AddressesMap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
