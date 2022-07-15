using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalAgente;
using System.Data;
using LoyalIGWEB.Models;

namespace LoyalIGWEB.Controllers
{
    public class ListadoDetalleController : Controller
    {
        // GET: ListadoDetalle
        public ActionResult ListadoCotizaciones()
        {
            PantallaListadoCotizaciones oPantallaListadoCotizaciones = new PantallaListadoCotizaciones();

            Consultas oConsultas = new Consultas();

            DataTable dtCotizaciones = oConsultas.ListarCotizaciones(Convert.ToInt32(this.Session["_CodigoAgente"]),"");

            List<ListadoCotizaciones> lsListadoCotizaciones = new List<ListadoCotizaciones>();
            lsListadoCotizaciones = (from DataRow drCotizaciones in dtCotizaciones.Rows
                                       select new ListadoCotizaciones()
                                       {
                                           CodigoCotizacion = Convert.ToInt32(drCotizaciones["CodigoCotizacion"]),
                                           FechaCotizacion = Convert.ToDateTime(drCotizaciones["FechaCotizacion"]),
                                           CodigoEstadoCotizacion = drCotizaciones["CodigoEstadoCotizacion"].ToString(),
                                           DescripcionEstadoCotizacion = drCotizaciones["DescripcionEstadoCotizacion"].ToString(),
                                           NombreCompleto = drCotizaciones["NombreCompleto"].ToString(),
                                           FechaNacimientoTitular = Convert.ToDateTime(drCotizaciones["FechaNacimientoTitular"]),
                                           EdadTitular = Convert.ToInt32(drCotizaciones["EdadTitular"]),
                                           FechaNacimientoConyuge = drCotizaciones["FechaNacimientoConyuge"].ToString(),
                                           EdadConyuge = drCotizaciones["EdadConyuge"].ToString(),
                                           CorreoElectronico = drCotizaciones["CorreoElectronico"].ToString(),
                                           Telefono = drCotizaciones["Telefono"].ToString(),
                                           Dependientes = Convert.ToInt32(drCotizaciones["Dependientes"]),
                                           Maternidad = drCotizaciones["Maternidad"].ToString(),
                                           Trasplante = drCotizaciones["Trasplante"].ToString(),
                                           SexoTitular = drCotizaciones["SexoTitular"].ToString(),
                                           SexoConyuge = drCotizaciones["SexoConyuge"].ToString(),
                                           FechaHoraCotizacion = Convert.ToDateTime(drCotizaciones["FechaHoraCotizacion"]),
                                           DescripcionPais = drCotizaciones["DescripcionPais"].ToString()

                                       }).ToList();

            oPantallaListadoCotizaciones.lstListadoCotizaciones = lsListadoCotizaciones;

            return View(oPantallaListadoCotizaciones);
        }

        public ActionResult ListadoPolizasActivas()
        {
            PantallaListadoPolizasActivas oPantallaListadoPolizasActivas = new PantallaListadoPolizasActivas();

            Consultas oConsultas = new Consultas();

            DataTable dtPolizasActivas = oConsultas.ListarPolizasActivas(Convert.ToInt32(this.Session["_CodigoAgente"]));

            List<ListadoPolizasActivas> lsListadoPolizasActivas = new List<ListadoPolizasActivas>();
            lsListadoPolizasActivas = (from DataRow drPolizasActivas in dtPolizasActivas.Rows
                                     select new ListadoPolizasActivas()
                                     {
                                        CodigoCertificado =  Convert.ToInt32(drPolizasActivas["CodigoCertificado"]),
                                        NumeroPoliza = drPolizasActivas["NumeroPoliza"].ToString(),
                                        DescripcionTipoVenta = drPolizasActivas["DescripcionTipoVenta"].ToString(),
                                        NombreCompleto = drPolizasActivas["NombreCompleto"].ToString(),
                                        NumeroAsegurados = drPolizasActivas["NumeroAsegurados"].ToString(),
                                        DescripcionPoliza = drPolizasActivas["DescripcionPoliza"].ToString(),
                                        DescripcionPais = drPolizasActivas["DescripcionPais"].ToString(),
                                        DescripcionEstadoCertificado = drPolizasActivas["DescripcionEstadoCertificado"].ToString(),
                                        FechaInicioVigencia = Convert.ToDateTime(drPolizasActivas["FechaInicioVigencia"]).ToString("MM/dd/yyyy"),
                                        DescripcionFormaPago = drPolizasActivas["DescripcionFormaPago"].ToString(),
                                        Prima = Convert.ToDecimal(drPolizasActivas["Prima"]),
                                        DescripcionPlan = drPolizasActivas["DescripcionPlan"].ToString(),
                                     }).ToList();

            oPantallaListadoPolizasActivas.lsListadoPolizasActivas = lsListadoPolizasActivas;

            return View(oPantallaListadoPolizasActivas);
        }
    }
}