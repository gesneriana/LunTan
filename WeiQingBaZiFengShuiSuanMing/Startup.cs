using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeiQingBaZiFengShuiSuanMing.Startup))]
namespace WeiQingBaZiFengShuiSuanMing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
