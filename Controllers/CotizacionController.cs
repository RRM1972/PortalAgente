using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyalIGWEB.Models;
using System.Data;
using PortalAgente;

namespace LoyalIGWEB.Controllers
{
    public class CotizacionController : Controller
    {
        // GET: Cotizacion
        public ActionResult SolicitarCotizacion(int nCodigoCotizacion)
        {
            Consultas oConsultas = new Consultas();
            SolicitarCotizacion oSolicitarCotizacion = new SolicitarCotizacion();

            if(nCodigoCotizacion == 0)
            {
                oSolicitarCotizacion.CodigoCotizacion = 0;
                oSolicitarCotizacion.FechaInicioValidez = DateTime.Now;
                oSolicitarCotizacion.SexoSolicitante = "M";
                oSolicitarCotizacion.FechaNacmimientoSolicitante = DateTime.Now;
            }
            else
            {
                DataTable dtCotizacion = oConsultas.CotizacionConsultaCabecera(nCodigoCotizacion);

                oSolicitarCotizacion.CodigoCotizacion = nCodigoCotizacion;
                oSolicitarCotizacion.CodigoAgente = Convert.ToInt32(dtCotizacion.Rows[0]["CodigoAgente"]);
                oSolicitarCotizacion.FechaInicioValidez = Convert.ToDateTime(dtCotizacion.Rows[0]["FechaInicioVigencia"]);
                oSolicitarCotizacion.CodigoPais = Convert.ToInt32(dtCotizacion.Rows[0]["CodigoPais"]);
                oSolicitarCotizacion.NombreSolicitante = dtCotizacion.Rows[0]["NombreCompleto"].ToString();
                oSolicitarCotizacion.FechaNacmimientoSolicitante = Convert.ToDateTime(dtCotizacion.Rows[0]["FechaNacimientoTitular"]);
                oSolicitarCotizacion.SexoSolicitante = dtCotizacion.Rows[0]["SexoTitular"].ToString();
                oSolicitarCotizacion.Correo = dtCotizacion.Rows[0]["CorreoElectronico"].ToString();

                if(dtCotizacion.Rows[0]["FechaNacimientoTitular"] == DBNull.Value)
                { oSolicitarCotizacion.IndicadorConyuge = false; }
                else
                { 
                    oSolicitarCotizacion.IndicadorConyuge = false;
                    oSolicitarCotizacion.FechaNacmimientoConyuge = Convert.ToDateTime(dtCotizacion.Rows[0]["FechaNacimientoTitular"]);
                    oSolicitarCotizacion.SexoConyuge = dtCotizacion.Rows[0]["SexoTitular"].ToString();
                }


                oSolicitarCotizacion.NumeroDependientes = Convert.ToInt32(dtCotizacion.Rows[0]["Dependientes"]);

                if(dtCotizacion.Rows[0]["Trasplante"].ToString() == "1")
                { oSolicitarCotizacion.TrasplanteOrganos = true; }
                else
                { oSolicitarCotizacion.TrasplanteOrganos = false; }

                if (dtCotizacion.Rows[0]["Maternidad"].ToString() == "1")
                { oSolicitarCotizacion.ComplicacionesMaternidad = true; }
                else
                { oSolicitarCotizacion.ComplicacionesMaternidad = false; }

            }


            ConsultasMaestro oConsultasMaestros = new ConsultasMaestro();

            DataTable dtPaises = oConsultasMaestros.ListarPaisesCotizacion();

            List<Paises> lsPaises = new List<Paises>();
            lsPaises = (from DataRow drPaises in dtPaises.Rows
                        select new Paises()
                        {
                            CodigoPais = Convert.ToInt32(drPaises["CodigoPais"]),
                            DescripcionPais = drPaises["DescripcionPais"].ToString(),

                        }).ToList();


            DataTable dtAgentes = oConsultasMaestros.ListarAgentesCotizacion(Convert.ToInt32(this.Session["_CodigoAgente"]));

            List<Agentes> lsAgentes = new List<Agentes>();
            lsAgentes = (from DataRow drAgentes in dtAgentes.Rows
                         select new Agentes()
                         {
                             CodigoAgente = Convert.ToInt32(drAgentes["CodigoAgente"]),
                             NombreAgente = drAgentes["NombreAgente"].ToString(),

                         }).ToList();


            oSolicitarCotizacion.ListarDependientes = ListaDependientes();
            oSolicitarCotizacion.ListarPaises = lsPaises;
            oSolicitarCotizacion.ListarAgentes = lsAgentes;

            return View(oSolicitarCotizacion);
        }

        public ActionResult ConsultaCotizacion(int nCodigoCotizacion)
        {

            Consultas oConsulta = new Consultas();

            DataTable dtCotizacion = oConsulta.CotizacionConsultaCabecera(nCodigoCotizacion);

            ConsultaCotizacion oConsultaCotizacion = new ConsultaCotizacion();

            oConsultaCotizacion.CodigoCotizacion = nCodigoCotizacion;
            oConsultaCotizacion.CodigoEstadoCotizacion = dtCotizacion.Rows[0]["CodigoEstadoCotizacion"].ToString();
            oConsultaCotizacion.DescripcionEstadoCotizacion = dtCotizacion.Rows[0]["DescripcionEstadoCotizacion"].ToString();
            oConsultaCotizacion.NombreSolicitante = dtCotizacion.Rows[0]["NombreCompleto"].ToString();
            oConsultaCotizacion.FechaNacimnientoSolicitante = Convert.ToDateTime(dtCotizacion.Rows[0]["FechaNacimientoTitular"]).ToString("MM/dd/yyyy");
            oConsultaCotizacion.EdadSolicitante = Convert.ToInt32(dtCotizacion.Rows[0]["EdadTitular"]);
            oConsultaCotizacion.SexoSolicitante = dtCotizacion.Rows[0]["SexoTitular"].ToString();
            oConsultaCotizacion.Correo = dtCotizacion.Rows[0]["CorreoElectronico"].ToString();
            oConsultaCotizacion.DescripcionPais = dtCotizacion.Rows[0]["DescripcionPais"].ToString();
            oConsultaCotizacion.ComplicacionesMaternidad = dtCotizacion.Rows[0]["Maternidad"].ToString();
            oConsultaCotizacion.TrasplanteOrganos = dtCotizacion.Rows[0]["Trasplante"].ToString();
            oConsultaCotizacion.NumeroDependientes = Convert.ToInt32(dtCotizacion.Rows[0]["Dependientes"]);
            if (dtCotizacion.Rows[0]["FechaNacimientoConyuge"] == DBNull.Value)
            {
                oConsultaCotizacion.FechaNacimnientoConyuge = "";
                oConsultaCotizacion.EdadConyuge = 0;
                oConsultaCotizacion.SexoConyuge = "";
            }
            else
            {
                oConsultaCotizacion.FechaNacimnientoConyuge = Convert.ToDateTime(dtCotizacion.Rows[0]["FechaNacimientoConyuge"]).ToString("MM/dd/yyyy");
                oConsultaCotizacion.EdadConyuge = Convert.ToInt32(dtCotizacion.Rows[0]["EdadConyuge"]);
                oConsultaCotizacion.SexoConyuge = dtCotizacion.Rows[0]["SexoConyuge"].ToString();

            }

            oConsultaCotizacion.ListaPrimasAnualBeyond = ListarPrimas(nCodigoCotizacion, 1, 1);
            oConsultaCotizacion.ListaPrimasSemiAnualBeyond = ListarPrimas(nCodigoCotizacion, 1, 2);
            oConsultaCotizacion.ListaPrimasTrimestralBeyond = ListarPrimas(nCodigoCotizacion, 1, 3);

            oConsultaCotizacion.ListaPrimasAnualPrivilege = ListarPrimas(nCodigoCotizacion, 2, 1);
            oConsultaCotizacion.ListaPrimasSemiAnualPrivilege = ListarPrimas(nCodigoCotizacion, 2, 2);
            oConsultaCotizacion.ListaPrimasTrimestralPrivilege = ListarPrimas(nCodigoCotizacion, 2, 3);

            oConsultaCotizacion.ListaPrimasAnualLiberty = ListarPrimas(nCodigoCotizacion, 3, 1);
            oConsultaCotizacion.ListaPrimasSemiAnualLiberty = ListarPrimas(nCodigoCotizacion, 3, 2);
            oConsultaCotizacion.ListaPrimasTrimestralLiberty = ListarPrimas(nCodigoCotizacion, 3, 3);

            oConsultaCotizacion.ListaPrimasAnualLegacy = ListarPrimas(nCodigoCotizacion, 4, 1);
            oConsultaCotizacion.ListaPrimasSemiAnualLegacy = ListarPrimas(nCodigoCotizacion, 4, 2);
            oConsultaCotizacion.ListaPrimasTrimestralLegacy = ListarPrimas(nCodigoCotizacion, 4, 3);

            return View(oConsultaCotizacion);
        }

        private List<PrimasConsulta> ListarPrimas(int nCodigoCotizacion, int nCodigoProducto, int nCodigoFormaPago)
        {
            string sFormaPago = "";

            if (nCodigoFormaPago == 1)
            { sFormaPago = "Anual"; }

            if (nCodigoFormaPago == 2)
            { sFormaPago = "Semestral"; }

            if (nCodigoFormaPago == 3)
            { sFormaPago = "Trimestral"; }

            Consultas oConsulta = new Consultas();

            DataTable dtPrimas = oConsulta.ObtenerPrimasSaludPDF(nCodigoCotizacion, nCodigoProducto, nCodigoFormaPago);

            List<PrimasConsulta> lsPrimas = new List<PrimasConsulta>();
            lsPrimas = (from DataRow drPrimas in dtPrimas.Rows
                        select new PrimasConsulta()
                        {
                            Cobertura = drPrimas["CodigoCobertura"].ToString(),
                            TipoPersona = drPrimas["CodigoTipoPersona"].ToString(),
                            FormaPago = sFormaPago,
                            Opcion1 = drPrimas["Opcion1"].ToString(),
                            Opcion2 = drPrimas["Opcion2"].ToString(),
                            Opcion3 = drPrimas["Opcion3"].ToString(),
                            Opcion4 = drPrimas["Opcion4"].ToString(),
                            Opcion5 = drPrimas["Opcion5"].ToString(),
                            Opcion6 = drPrimas["Opcion6"].ToString()
                        }).ToList();

            return lsPrimas;
        }
        private List<Dependientes> ListaDependientes()
        {
            List<Dependientes> lsDependientes = new List<Dependientes>();

            lsDependientes.Add(new Dependientes());
            lsDependientes[0].NumeroDependiente = 0;
            lsDependientes[0].DescripcionNumeroDependiente = "Sin Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[1].NumeroDependiente = 1;
            lsDependientes[1].DescripcionNumeroDependiente = "1 Dependiente";

            lsDependientes.Add(new Dependientes());
            lsDependientes[2].NumeroDependiente = 2;
            lsDependientes[2].DescripcionNumeroDependiente = "2 Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[3].NumeroDependiente = 3;
            lsDependientes[3].DescripcionNumeroDependiente = "3 Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[4].NumeroDependiente = 4;
            lsDependientes[4].DescripcionNumeroDependiente = "4 Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[5].NumeroDependiente = 5;
            lsDependientes[5].DescripcionNumeroDependiente = "5 Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[6].NumeroDependiente = 6;
            lsDependientes[6].DescripcionNumeroDependiente = "6 Dependientes";

            lsDependientes.Add(new Dependientes());
            lsDependientes[7].NumeroDependiente = 7;
            lsDependientes[7].DescripcionNumeroDependiente = "7 Dependientes";

            return lsDependientes;
        }

        [HttpPost]
        public ActionResult SolicitarCotizacion(SolicitarCotizacion oSolicitarCotizacion)
        {
            int nCodigoCotizacion;
            FormularioCotizacion oFormularioCotizacion = new FormularioCotizacion();

            oFormularioCotizacion.CodigoCotizacion = oSolicitarCotizacion.CodigoCotizacion;
            oFormularioCotizacion.FechaCotizacion = DateTime.Now;
            oFormularioCotizacion.FechaInicioVigencia = oSolicitarCotizacion.FechaInicioValidez;
            oFormularioCotizacion.Nombre = oSolicitarCotizacion.NombreSolicitante;
            oFormularioCotizacion.ApellidoPaterno = "";
            oFormularioCotizacion.ApellidoMaterno = "";
            oFormularioCotizacion.FechaNacimiento = oSolicitarCotizacion.FechaNacmimientoSolicitante;
            oFormularioCotizacion.Pais = oSolicitarCotizacion.CodigoPais;
            oFormularioCotizacion.SexoContratante = oSolicitarCotizacion.SexoSolicitante;

            oFormularioCotizacion.IndicadorConyuge = oSolicitarCotizacion.IndicadorConyuge;
            if(oSolicitarCotizacion.IndicadorConyuge)
            {
                oFormularioCotizacion.FechaNacimientoConyuge = oSolicitarCotizacion.FechaNacmimientoConyuge;
                oFormularioCotizacion.SexoConyuge = oSolicitarCotizacion.SexoConyuge;
            }
            else
            {
                oFormularioCotizacion.FechaNacimientoConyuge = DateTime.Now;
                oFormularioCotizacion.SexoConyuge = "";
            }


            oFormularioCotizacion.Dependientes = oSolicitarCotizacion.NumeroDependientes;
            oFormularioCotizacion.Correo = oSolicitarCotizacion.Correo;

            if (oSolicitarCotizacion.TrasplanteOrganos)
            { oFormularioCotizacion.Trasplante = "1"; }
            else
            { oFormularioCotizacion.Trasplante = "0"; }

            if (oSolicitarCotizacion.ComplicacionesMaternidad)
            { oFormularioCotizacion.Maternidad = "1"; }
            else
            { oFormularioCotizacion.Maternidad = "0"; }

            oFormularioCotizacion.SumaAseguradaPrincipal = 0;
            oFormularioCotizacion.SumaAseguradaConyuge = 0;
            oFormularioCotizacion.CodigoAgente = oSolicitarCotizacion.CodigoAgente;
            oFormularioCotizacion.CodigoUsuario = Convert.ToInt32(Session["_CodigoUsuario"]);

            Cotizaciones oCotizaciones = new Cotizaciones();

            nCodigoCotizacion = oCotizaciones.GenerarCotizacionSalud(oFormularioCotizacion);

            return RedirectToAction("ConsultaCotizacion", new { nCodigoCotizacion = nCodigoCotizacion });
        }
    }
}