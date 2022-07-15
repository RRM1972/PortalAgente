using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LoyalIGWEB.Models;
using PortalAgente;

namespace LoyalIGWEB.Controllers
{
    public class AgentesController : Controller
    {
        // GET: Agentes
        public ActionResult PerfilAgente()
        {
            int nCodigoAgente = Convert.ToInt32(this.Session["_CodigoAgente"]);
            PerfilAgente oPerfilAgente = new PerfilAgente();
            Agente oDatosAgente = new Agente();
            Consultas oConsultas = new Consultas();

            DataTable dtAgente = oConsultas.ObtenerAgenteDetalle(nCodigoAgente);

            oDatosAgente.CodigoAgente = Convert.ToInt32(dtAgente.Rows[0]["CodigoAgente"]);
            oDatosAgente.CodigoPersona = Convert.ToInt32(dtAgente.Rows[0]["CodigoPersona"]);
            oDatosAgente.Nombre = dtAgente.Rows[0]["Nombre"].ToString();
            oDatosAgente.ApellidoPaterno = dtAgente.Rows[0]["ApellidoPaterno"].ToString();
            oDatosAgente.ApellidoMaterno = dtAgente.Rows[0]["ApellidoMaterno"].ToString();
            oDatosAgente.NombreCompleto = dtAgente.Rows[0]["NombreCompleto"].ToString();
            oDatosAgente.NombreAgente = dtAgente.Rows[0]["NombreAgente"].ToString();
            oDatosAgente.Sexo = dtAgente.Rows[0]["Sexo"].ToString();
            oDatosAgente.FechaNacimiento = Convert.ToDateTime(dtAgente.Rows[0]["FechaNacimiento"]);
            oDatosAgente.Edad = Convert.ToInt32(dtAgente.Rows[0]["Edad"]);
            if(dtAgente.Rows[0]["CodigoAgenteDependencia"] != DBNull.Value)
            { oDatosAgente.CodigoAgenteDependencia = Convert.ToInt32(dtAgente.Rows[0]["CodigoAgenteDependencia"]); }
            else
            { oDatosAgente.CodigoAgenteDependencia = 0; }
            
            oDatosAgente.NombreAgenteDependencia = dtAgente.Rows[0]["NombreAgenteDependencia"].ToString();
            oDatosAgente.CodigoTipoAgente = Convert.ToInt32(dtAgente.Rows[0]["CodigoTipoAgente"]);
            oDatosAgente.DescripcionTipoAgente = dtAgente.Rows[0]["DescripcionTipoAgente"].ToString();

            //oDatosAgente.CodigoNivel = Convert.ToInt32(dtAgente.Rows[0]["CodigoNivel"]);

            oDatosAgente.CodigoPais = Convert.ToInt32(dtAgente.Rows[0]["CodigoPais"]);
            oDatosAgente.DescripcionPais = dtAgente.Rows[0]["DescripcionPais"].ToString();
            oDatosAgente.Talla = dtAgente.Rows[0]["Talla"].ToString();
            oDatosAgente.CodigoCorreo = Convert.ToInt32(dtAgente.Rows[0]["CodigoCorreo"]);
            oDatosAgente.Correo = dtAgente.Rows[0]["Correo"].ToString();
            oDatosAgente.CodigoCelular = Convert.ToInt32(dtAgente.Rows[0]["CodigoCelular"]);
            oDatosAgente.Celular = dtAgente.Rows[0]["Celular"].ToString();
            oDatosAgente.CodigoTelefono = Convert.ToInt32(dtAgente.Rows[0]["CodigoTelefono"]);
            oDatosAgente.Telefono = dtAgente.Rows[0]["Telefono"].ToString();
            oDatosAgente.CodigoTelefonoCasa = Convert.ToInt32(dtAgente.Rows[0]["CodigoTelefonoCasa"]);
            oDatosAgente.TelefonoCasa = dtAgente.Rows[0]["TelefonoCasa"].ToString();
            oDatosAgente.CodigoDireccionPersona = Convert.ToInt32(dtAgente.Rows[0]["CodigoDireccionPersona"]);
            oDatosAgente.Direccion = dtAgente.Rows[0]["Direccion"].ToString();
            oDatosAgente.Ciudad = dtAgente.Rows[0]["Ciudad"].ToString();
            oDatosAgente.Provincia = dtAgente.Rows[0]["Provincia"].ToString();
            oDatosAgente.CodigoPostal = dtAgente.Rows[0]["CodigoPostal"].ToString();
            oDatosAgente.CodigoDireccionOficina = Convert.ToInt32(dtAgente.Rows[0]["CodigoDireccionOficina"]);
            oDatosAgente.DireccionOficina = dtAgente.Rows[0]["DireccionOficina"].ToString();
            oDatosAgente.CiudadOficina = dtAgente.Rows[0]["CiudadOficina"].ToString();
            oDatosAgente.ProvinciaOficina = dtAgente.Rows[0]["ProvinciaOficina"].ToString();
            oDatosAgente.CodigoPostalOficina = dtAgente.Rows[0]["CodigoPostalOficina"].ToString();
            //oDatosAgente.CodigoPaisOficina = Convert.ToInt32(dtAgente.Rows[0]["CodigoPaisOficina"]);
            oDatosAgente.DescripcionPaisOficina = dtAgente.Rows[0]["DescripcionPaisOficina"].ToString();
            oDatosAgente.CodigoDireccionPostal = Convert.ToInt32(dtAgente.Rows[0]["CodigoDireccionPostal"]);
            oDatosAgente.DireccionPostal = dtAgente.Rows[0]["DireccionPostal"].ToString();
            oDatosAgente.CiudadPostal = dtAgente.Rows[0]["CiudadPostal"].ToString();
            oDatosAgente.ProvinciaPostal = dtAgente.Rows[0]["ProvinciaPostal"].ToString();
            oDatosAgente.CodigoPostalPostal = dtAgente.Rows[0]["CodigoPostalPostal"].ToString();
            //oDatosAgente.CodigoPaisPostal = Convert.ToInt32(dtAgente.Rows[0]["CodigoPaisPostal"]);
            oDatosAgente.DescripcionPaisPostal = dtAgente.Rows[0]["DescripcionPaisPostal"].ToString();
            oDatosAgente.CodigoUsuario = Convert.ToInt32(dtAgente.Rows[0]["CodigoUsuario"]);
            oDatosAgente.CodigoAgenciaMaster = Convert.ToInt32(dtAgente.Rows[0]["CodigoAgenciaMaster"]);
            oDatosAgente.AgenciaMaster = dtAgente.Rows[0]["AgenciaMaster"].ToString();
            oDatosAgente.CodigoBanco = Convert.ToInt32(dtAgente.Rows[0]["CodigoBanco"]);
            oDatosAgente.DescripcionBanco = dtAgente.Rows[0]["NombreTitularCuenta"].ToString();
            oDatosAgente.NombreTitularCuenta = dtAgente.Rows[0]["NombreTitularCuenta"].ToString();
            oDatosAgente.NumeroCuentaDeposito = dtAgente.Rows[0]["NumeroCuentaDeposito"].ToString();
            oDatosAgente.CodigoRouting = dtAgente.Rows[0]["CodigoRouting"].ToString();
            oDatosAgente.DirecciónCuenta = dtAgente.Rows[0]["DirecciónCuenta"].ToString();
            oDatosAgente.TipoCuentaDeposito = dtAgente.Rows[0]["TipoCuentaDeposito"].ToString();

            oPerfilAgente.DatosAgente = oDatosAgente;
            DataTable dtAgentesMaster = oConsultas.ListarAgentesMaster(nCodigoAgente);

            List<DetalleListaAgentesMaster> lsListaAgente = new List<DetalleListaAgentesMaster>();
            lsListaAgente = (from DataRow drAgentesMaster in dtAgentesMaster.Rows
                           select new DetalleListaAgentesMaster()
                           {
                               CodigoAgente = Convert.ToInt32(drAgentesMaster["CodigoAgente"]),
                               NombreAgente = drAgentesMaster["NombreAgente"].ToString(),
                               Polizas = Convert.ToInt32(drAgentesMaster["CodigoAgente"]),
                               Primas = Convert.ToDecimal(drAgentesMaster["Primas"]),
                               Objetivo = Convert.ToDecimal(drAgentesMaster["Objetivo"]),
                               AvanceObjetivo = Convert.ToDecimal(drAgentesMaster["AvanceObjetivo"]),
                               ComisionPropia = Convert.ToDecimal(drAgentesMaster["ComisionPropia"]),
                               ComisionMaster = Convert.ToDecimal(drAgentesMaster["ComisionMaster"]),

                           }).ToList();

            oPerfilAgente.ListaAgentes = lsListaAgente;

            DataTable dtNuevoNegocio = oConsultas.ListarComisionesAgente(nCodigoAgente,"01");

            List<PorcentajeComisionesAgente> lsNuevoNegocio = new List<PorcentajeComisionesAgente>();
            lsNuevoNegocio = (from DataRow drNuevoNegocio in dtNuevoNegocio.Rows
                             select new PorcentajeComisionesAgente()
                             {
                                CodigoAgente = Convert.ToInt32(drNuevoNegocio["CodigoAgente"]),
                                 CodigoAgenteComision = Convert.ToInt32(drNuevoNegocio["CodigoAgenteComision"]),
                                 NombreCompleto = drNuevoNegocio["NombreCompleto"].ToString(),
                                 NombreAgente = drNuevoNegocio["NombreAgente"].ToString(),
                                 CodigoPoliza = Convert.ToInt32(drNuevoNegocio["CodigoPoliza"]),
                                 DescripcionPoliza = drNuevoNegocio["DescripcionPoliza"].ToString(),
                                 CodigoTipoVenta = drNuevoNegocio["CodigoTipoVenta"].ToString(),
                                 DescripcionTipoVenta = drNuevoNegocio["DescripcionTipoVenta"].ToString(),
                                 PorcentajeComision = Convert.ToDecimal(drNuevoNegocio["PorcentajeComision"]),
                             }).ToList();

            oPerfilAgente.ComisionesNuevoNegocio = lsNuevoNegocio;

            return View(oPerfilAgente);
        }
    }
}