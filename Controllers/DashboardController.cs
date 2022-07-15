using LoyalIGWEB.Models;
using System.Collections.Generic;
using System.Linq;
using PortalAgente;
using System;
using System.Data;
using System.Web.Mvc;

namespace LoyalIGWEB.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Principal()
        {
            DateTime dFechaInicio = new DateTime(2020, 1, 1);
            DateTime dFechaFin = new DateTime(2022, 12, 31);
            DateTime dFechaInicioComparado = new DateTime(2020, 1, 1);
            DateTime dFechaFinComparado = new DateTime(2022, 12, 31);

            Consultas oConsultas = new Consultas();

            DashBoardPrincipal oDashBoardPrincipal = new DashBoardPrincipal();

            int nCodigoAgente = Convert.ToInt32(this.Session["_CodigoAgente"]);
            DataTable dtDashboardPrincipal = oConsultas.DashBoardPrincipal(dFechaInicio, dFechaFin, dFechaInicioComparado, dFechaFinComparado, nCodigoAgente);

            oDashBoardPrincipal.CodigoAgente = nCodigoAgente;
            //oDashBoardPrincipal.NombreAgente = dtDashboardPrincipal.Rows[0]["NombreAgente"].ToString();
            oDashBoardPrincipal.NombreAgente = "Demo";
            oDashBoardPrincipal.CodigoInternoAgente = dtDashboardPrincipal.Rows[0]["CodigoInternoAgente"].ToString();
            //oDashBoardPrincipal.NombreCompleto = dtDashboardPrincipal.Rows[0]["NombreCompleto"].ToString();
            oDashBoardPrincipal.NombreCompleto = "Demo";
            oDashBoardPrincipal.Cargo = dtDashboardPrincipal.Rows[0]["Cargo"].ToString();
            oDashBoardPrincipal.Direccion = dtDashboardPrincipal.Rows[0]["Direccion"].ToString();
            oDashBoardPrincipal.Celular = dtDashboardPrincipal.Rows[0]["Celular"].ToString();
            oDashBoardPrincipal.Telefono = dtDashboardPrincipal.Rows[0]["Telefono"].ToString();
            oDashBoardPrincipal.Email = dtDashboardPrincipal.Rows[0]["Email"].ToString();
            oDashBoardPrincipal.TotalCotizaciones = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["TotalCotizaciones"]);
            oDashBoardPrincipal.TotalSolicitudesIngresadas = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["TotalSolicitudesIngresadas"]);
            oDashBoardPrincipal.TotalPolizasActivas = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["TotalPolizasActivas"]);
            oDashBoardPrincipal.PolizasNuevoNegocio = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["PolizasNuevoNegocio"]);
            oDashBoardPrincipal.PolizasRenovaciones = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["PolizasRenovaciones"]);
            oDashBoardPrincipal.PolizasCanceladas = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["PolizasCanceladas"]);
            oDashBoardPrincipal.PolizasPrimerPago = Convert.ToInt32(dtDashboardPrincipal.Rows[0]["PolizasPrimerPago"]);
            oDashBoardPrincipal.TotalPrimas = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalPrimas"]);
            oDashBoardPrincipal.TotalPrimasPagadas = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalPrimasPagadas"]);
            if(Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalPrimasPagadas"]) > 0)
            { oDashBoardPrincipal.PorcentajePrimasPagadas = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalPrimasPagadas"]) / Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalPrimas"]); }
            else
            { oDashBoardPrincipal.PorcentajePrimasPagadas = 0; }                
            oDashBoardPrincipal.PrimasNuevoNegocio = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["PrimasNuevoNegocio"]);
            oDashBoardPrincipal.PrimasRenovaciones = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["PrimasRenovaciones"]);
            oDashBoardPrincipal.TotalComisiones = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["TotalComisiones"]);
            oDashBoardPrincipal.ComisionesNuevoNegocio = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesNuevoNegocio"]);
            oDashBoardPrincipal.ComisionesNuevoNegocioVentaPropia = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesNuevoNegocioVentaPropia"]);
            oDashBoardPrincipal.ComisionesNuevoNegocioOtrosAgentes = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesNuevoNegocioOtrosAgentes"]);
            oDashBoardPrincipal.ComisionesRenovaciones = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesRenovaciones"]);
            oDashBoardPrincipal.ComisionesRenovacionesVentaPropia = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesRenovacionesVentaPropia"]);
            oDashBoardPrincipal.ComisionesRenovacionesOtrosAgentes = Convert.ToDecimal(dtDashboardPrincipal.Rows[0]["ComisionesRenovacionesOtrosAgentes"]);

            DataTable dtSolicitudesEstado = oConsultas.SolicitudesEstadoAgente(dFechaInicio, dFechaFin, dFechaInicioComparado, dFechaFinComparado, nCodigoAgente);

            List<SolicitudesIngresadas> lsSolicitudesIngresadas = new List<SolicitudesIngresadas>();
            lsSolicitudesIngresadas = (from DataRow drSolicitudesEstado in dtSolicitudesEstado.Rows
                                    select new SolicitudesIngresadas()
                                    {
                                        CodigoEstado = drSolicitudesEstado["CodigoEstadoSolicitud"].ToString(),
                                        Estado = drSolicitudesEstado["DescripcionEstadoSolicitud"].ToString(),
                                        Cantidad = Convert.ToInt32(drSolicitudesEstado["Cantidad"]),
                                        Factor = Convert.ToDecimal(drSolicitudesEstado["Cantidad"]) / oDashBoardPrincipal.TotalSolicitudesIngresadas,
                                        Porcentaje = (Convert.ToDecimal(drSolicitudesEstado["Cantidad"]) / oDashBoardPrincipal.TotalSolicitudesIngresadas) * 100

                                    }).ToList();

            oDashBoardPrincipal.lsSolicitudesIngresadas = lsSolicitudesIngresadas;

            DataTable dtCotizacionesEstado = oConsultas.CotizacionesEstadoAgente(dFechaInicio, dFechaFin, dFechaInicioComparado, dFechaFinComparado, nCodigoAgente);

            List<CotizacionesEstado> lsCotizacionEstado = new List<CotizacionesEstado>();
            lsCotizacionEstado = (from DataRow drCotizacionesEstado in dtCotizacionesEstado.Rows
                                       select new CotizacionesEstado()
                                       {
                                           CodigoEstadoCotizacion = drCotizacionesEstado["CodigoEstadoCotizacion"].ToString(),
                                           DescripcionEstadoCotizacion = drCotizacionesEstado["DescripcionEstadoCotizacion"].ToString(),
                                           Cantidad = Convert.ToInt32(drCotizacionesEstado["Cantidad"]),

                                       }).ToList();

            oDashBoardPrincipal.lsCotizacionesEstado = lsCotizacionEstado;

            return View(oDashBoardPrincipal);
        }

        public ActionResult Cancun2023(int nCodigoAgente)
        {
            return View();
        }

        public ActionResult ListarComisionesDashboard()
        {
            Consultas oConsultas = new Consultas();

            ListarComisionesDashboard oListarComisionesDashboard = new ListarComisionesDashboard();

            DataTable dtListarComisionesDashboard = oConsultas.ListarComisiones(Convert.ToInt32(this.Session["_CodigoAgente"]));


            List<DetalleListaComisiones> lsListarComisionesDashboard = new List<DetalleListaComisiones>();
            lsListarComisionesDashboard = (from DataRow drListarComisionesDashboard in dtListarComisionesDashboard.Rows
                                  select new DetalleListaComisiones()
                                  {
                                    CodigoCicloComisiones = Convert.ToInt32(drListarComisionesDashboard["CodigoCicloComisiones"]),
                                    FechaInicio = Convert.ToDateTime(drListarComisionesDashboard["FechaInicio"]),
                                    FechaFin = Convert.ToDateTime(drListarComisionesDashboard["FechaFin"]),
                                    NumeroPoliza = drListarComisionesDashboard["NumeroPoliza"].ToString(),
                                    NombreCompleto = drListarComisionesDashboard["NombreCompleto"].ToString(),
                                    DescripcionTipoVenta = drListarComisionesDashboard["DescripcionTipoVenta"].ToString(),
                                    PrimaComisionable = Convert.ToInt32(drListarComisionesDashboard["PrimaComisionable"]),
                                    PorcentajeComision = Convert.ToInt32(drListarComisionesDashboard["PorcentajeComision"]),
                                    ValorComision = Convert.ToInt32(drListarComisionesDashboard["ValorComision"]),
                                    CodigoAgenteGenera = Convert.ToInt32(drListarComisionesDashboard["CodigoAgenteGenera"]),
                                    NombreAgenteGenera = drListarComisionesDashboard["NombreAgenteGenera"].ToString(),
                                  }).ToList();

            oListarComisionesDashboard.ListaComisiones = lsListarComisionesDashboard;

            return View(oListarComisionesDashboard);
        }
    }
}