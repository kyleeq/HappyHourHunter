using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MealDeals.Startup))]
namespace MealDeals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
