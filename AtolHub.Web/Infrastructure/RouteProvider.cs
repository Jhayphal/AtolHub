using AtolHub.Framework.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AtolHub.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(name: "areaRoute", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("HomePage", "", new { controller = "Home", action = "Index" });
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
