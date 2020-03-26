using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, // Url/
                "",new
            {
                controller = "Book",action="List",
                specilization = (string)null,pageno=1
            });
            routes.MapRoute(null, // url/BookListPage2
                "BookListPage{pageno}", new
            {
                controller = "Book",
                action = "List",
                specilization = (string)null
            });

            routes.MapRoute(null // url/Information Systems
                ,"{specilization}", new
            {
                controller = "Book",
                action = "List",
                pageno = 1
            });

            routes.MapRoute(null  // url/IS/Page2
                , "{specilization}/Page{pageno}", new
            {
                controller = "Book",
                action = "List"
            }, new { pageno = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "list", id = UrlParameter.Optional }
            );
        }
    }
}
