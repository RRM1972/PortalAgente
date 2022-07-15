using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using PortalAgente;
using System.Data;

namespace LoyalIGWEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Download(int nCodigoDocumento)
        {
            DocumentosProceso oDocumentosProceso = new DocumentosProceso();

            DataTable dtDocumento = oDocumentosProceso.ObtenerDocumento(nCodigoDocumento);

            byte[] oDocumentoByte = (byte[])dtDocumento.Rows[0]["Documento"];
            string sNombreArchivo = dtDocumento.Rows[0]["NombreDocumento"].ToString();

            return File(oDocumentoByte, "application/pdf", sNombreArchivo);
        }

        public ActionResult DownloadCotizacion(int nCodigoCotizacion,int nCodigoProducto,int nCodigoUsuario,string sIdioma)
        {
            try
            {
                Cotizaciones oCotizaciones = new Cotizaciones();

                int nCodigoDocumento = oCotizaciones.ObtenerCodigoPDFCotizacion(nCodigoCotizacion, nCodigoProducto, nCodigoUsuario, sIdioma);

                DocumentosProceso oDocumentosProceso = new DocumentosProceso();

                DataTable dtDocumento = oDocumentosProceso.ObtenerDocumento(nCodigoDocumento);

                byte[] oDocumentoByte = (byte[])dtDocumento.Rows[0]["Documento"];
                string sNombreArchivo = dtDocumento.Rows[0]["NombreDocumento"].ToString();

                return File(oDocumentoByte, "application/pdf", sNombreArchivo);
            }

            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
            }
            
        }
    }
}