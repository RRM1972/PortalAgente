using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LoyalIGWEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("_CodigoUsuario", 0);
            HttpContext.Current.Session.Add("_NombreUsuario", "");
            HttpContext.Current.Session.Add("_NombreUsuarioPagina", "");
            HttpContext.Current.Session.Add("_CodigoPerfil", 0);
            HttpContext.Current.Session.Add("_NombrePerfil", "");
            HttpContext.Current.Session.Add("_CorreoUsuario", "");
            HttpContext.Current.Session.Add("_CodigoAgente", 0);
            HttpContext.Current.Session.Add("_NombreAgente", "");
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
        }
    }
}
