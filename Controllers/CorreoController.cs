using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyalIGWEB.Models;
using PortalAgente;

namespace LoyalIGWEB.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public ActionResult EnvioCorreo()
        {
            EnvioCorreo oEnvioCorreo = new EnvioCorreo();

            oEnvioCorreo.Para = "renato_ramirez@outlook.com";
            return View(oEnvioCorreo);
        }

        [HttpPost]
        public ActionResult EnvioCorreo(EnvioCorreo oEnvioCorreo)
        {
            CorreoLoyal oCorreoLoyal = new CorreoLoyal();

            try 
            {
                oCorreoLoyal.CorreoEnvioCotizacion(212, 1, 1, "renato_ramirez@outlook.com", "", "es");

                oEnvioCorreo.AsuntoCorreo = "";
                oEnvioCorreo.CuerpoCorreo = "";

                Cotizaciones oCotizacion = new Cotizaciones();

                //oCotizacion.ObtenerCodigoPDFCotizacion(177, 1, 1, "es");

                return View(oEnvioCorreo);
            }

                catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
            }
        }
    }


}