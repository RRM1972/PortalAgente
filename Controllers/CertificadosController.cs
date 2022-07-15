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
    public class CertificadosController : Controller
    {
        // GET: Certificados
        public ActionResult Consulta(int nCodigoCertificado)
        {
            Certificados oCertificados = new Certificados();

            DataTable dtCertificado = oCertificados.ObtenerCertificado(nCodigoCertificado);

            int nCodigoSolicitud = Convert.ToInt32(dtCertificado.Rows[0]["CodigoSolicitud"]);

            DataTable dtAsegurados = oCertificados.ListarAsegurados(nCodigoCertificado);
            DataTable dtDocumentos = oCertificados.DocumentosSolicitudClasificado(nCodigoSolicitud);
            DataTable dtPagos = oCertificados.ListarCuotasSolicitudesConsulta(nCodigoSolicitud);

            ConsultaCertificado oConsultaCertificado = new ConsultaCertificado();
            Asegurados oAsegurados = new Asegurados();
            Documento oDocumento = new Documento();
            Pagos oPagos = new Pagos();

            oConsultaCertificado.CodigoSolicitud = Convert.ToInt32(dtCertificado.Rows[0]["CodigoSolicitud"]);
            oConsultaCertificado.CodigoCertificado = Convert.ToInt32(dtCertificado.Rows[0]["CodigoCertificado"]);
            oConsultaCertificado.CodigoCotizacion = Convert.ToInt32(dtCertificado.Rows[0]["CodigoCotizacion"]);
            oConsultaCertificado.NumeroPoliza = dtCertificado.Rows[0]["NumeroPoliza"].ToString();
            oConsultaCertificado.CodigoEstadoCertificado = dtCertificado.Rows[0]["CodigoEstadoCertificado"].ToString();
            oConsultaCertificado.DescripcionEstadoCertificado = dtCertificado.Rows[0]["DescripcionEstadoCertificado"].ToString();
            oConsultaCertificado.CodigoEstadoSolicitud = dtCertificado.Rows[0]["CodigoEstadoSolicitud"].ToString();
            oConsultaCertificado.DescripcionEstadoSolicitud = dtCertificado.Rows[0]["DescripcionEstadoSolicitud"].ToString();
            oConsultaCertificado.FechaSolicitud = Convert.ToDateTime(dtCertificado.Rows[0]["FechaSolicitud"]);
            if(dtCertificado.Rows[0]["FechaAprobacion"] != DBNull.Value)
            { oConsultaCertificado.FechaAprobacion = Convert.ToDateTime(dtCertificado.Rows[0]["FechaAprobacion"]); }
            
            oConsultaCertificado.FechaInicioVigencia = Convert.ToDateTime(dtCertificado.Rows[0]["FechaInicioVigencia"]);
            oConsultaCertificado.FechaFinVigencia = Convert.ToDateTime(dtCertificado.Rows[0]["FechaFinVigencia"]);
            oConsultaCertificado.CodigoPlan = Convert.ToInt32(dtCertificado.Rows[0]["CodigoPlan"]);
            oConsultaCertificado.DescripcionPlan = dtCertificado.Rows[0]["DescripcionPlan"].ToString();
            oConsultaCertificado.DescripcionPlanesConsulta = dtCertificado.Rows[0]["DescripcionPlanesConsulta"].ToString();
            oConsultaCertificado.CodigoPoliza = Convert.ToInt32(dtCertificado.Rows[0]["CodigoPoliza"]);
            oConsultaCertificado.DescripcionPoliza = dtCertificado.Rows[0]["DescripcionPoliza"].ToString();
            oConsultaCertificado.CodigoProducto = Convert.ToInt32(dtCertificado.Rows[0]["CodigoProducto"]);
            oConsultaCertificado.DescripcionProducto = dtCertificado.Rows[0]["DescripcionProducto"].ToString();
            oConsultaCertificado.ColorPrincipal = dtCertificado.Rows[0]["ColorPrincipal"].ToString();
            oConsultaCertificado.NumeroAsegurados = Convert.ToInt32(dtCertificado.Rows[0]["NumeroAsegurados"]);
            oConsultaCertificado.CodigoPais = Convert.ToInt32(dtCertificado.Rows[0]["CodigoPais"]);
            oConsultaCertificado.DescripcionPais = dtCertificado.Rows[0]["DescripcionPais"].ToString();
            oConsultaCertificado.CodigoFormaPago = Convert.ToInt32(dtCertificado.Rows[0]["CodigoFormaPago"]);
            oConsultaCertificado.DescripcionFormaPago = dtCertificado.Rows[0]["DescripcionFormaPago"].ToString();
            oConsultaCertificado.NumeroDependientes = Convert.ToInt32(dtCertificado.Rows[0]["NumeroDependientes"]);
            oConsultaCertificado.IndicadorTrasplante = dtCertificado.Rows[0]["IndicadorTrasplante"].ToString();
            oConsultaCertificado.IndicadorMaternidad = dtCertificado.Rows[0]["IndicadorMaternidad"].ToString();
            oConsultaCertificado.CodigoAgente = Convert.ToInt32(dtCertificado.Rows[0]["CodigoAgente"]);
            oConsultaCertificado.NombreAgente = dtCertificado.Rows[0]["NombreAgente"].ToString();
            oConsultaCertificado.Prima = Convert.ToDecimal(dtCertificado.Rows[0]["Prima"]);
            oConsultaCertificado.PrimaComisionable = Convert.ToDecimal(dtCertificado.Rows[0]["PrimaComisionable"]);
            oConsultaCertificado.CostoAdministrativo = Convert.ToDecimal(dtCertificado.Rows[0]["CostoAdministrativo"]);
            oConsultaCertificado.CodigoTipoVenta = dtCertificado.Rows[0]["CodigoTipoVenta"].ToString();
            oConsultaCertificado.DescripcionTipoVenta = dtCertificado.Rows[0]["DescripcionTipoVenta"].ToString();
            oConsultaCertificado.PeriodoEspera = Convert.ToInt32(dtCertificado.Rows[0]["PeriodoEspera"]);

            List<Asegurados> lsAsegurados = new List<Asegurados>();
            lsAsegurados = (from DataRow drAsegurados in dtAsegurados.Rows
                           select new Asegurados()
                           {
                                CodigoSolicitud = Convert.ToInt32(drAsegurados["CodigoSolicitud"]),
                                CodigoPersonaSolicitud = Convert.ToInt32(drAsegurados["CodigoPersonaSolicitud"]),
                                CodigoPersona = Convert.ToInt32(drAsegurados["CodigoPersona"]),
                                CodigoCertificado = Convert.ToInt32(drAsegurados["CodigoCertificado"]),
                                Nombre = drAsegurados["Nombre"].ToString(),
                                ApellidoPaterno = drAsegurados["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = drAsegurados["ApellidoMaterno"].ToString(),
                                NombreCompleto = drAsegurados["NombreCompleto"].ToString(),
                                DescripcionTipoPersonaCotizacion  = drAsegurados["DescripcionTipoPersonaCotizacion"].ToString(),
                                CodigoSistemaMedida  = drAsegurados["CodigoSistemaMedida"].ToString(),
                                Talla = Convert.ToDecimal(drAsegurados["Talla"]),
                                Peso = Convert.ToDecimal(drAsegurados["Peso"]),
                                Imc = Convert.ToDecimal(drAsegurados["Imc"]),
                                DescripcionVinculo = drAsegurados["DescripcionVinculo"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(drAsegurados["FechaNacimiento"]),
                                Edad = Convert.ToInt32(drAsegurados["Edad"]),
                                DescripcionEstadoCivil = drAsegurados["DescripcionEstadoCivil"].ToString(),
                                Sexo = drAsegurados["Sexo"].ToString(),
                                CodigoTipoPersonaCertificado = drAsegurados["CodigoTipoPersonaCertificado"].ToString(),
                                IndicadorRestricciones = drAsegurados["IndicadorRestricciones"].ToString(),
                                CodigoTipoPeriodoTiempo = drAsegurados["CodigoTipoPeriodoTiempo"].ToString(),
                                PeriodoEspera = Convert.ToInt32(drAsegurados["PeriodoEspera"]),
                                DescripcionPeriodoTiempo = drAsegurados["DescripcionPeriodoTiempo"].ToString(),
                                FechaInicioVigencia = drAsegurados["FechaInicioVigencia"].ToString(),
                                FechaFinVigencia = drAsegurados["FechaFinVigencia"].ToString(),
                                CodigoCorreo = Convert.ToInt32(drAsegurados["CodigoCorreo"]),
                                Correo = drAsegurados["Correo"].ToString(),
                                CodigoCelular = Convert.ToInt32(drAsegurados["CodigoCelular"]),
                                Celular = drAsegurados["Celular"].ToString(),
                                CodigoTelefono = Convert.ToInt32(drAsegurados["CodigoTelefono"]),
                                DireccionPrincipal = drAsegurados["DireccionPrincipal"].ToString(),
                                CodigoPostalPrincipal = drAsegurados["CodigoPostalPrincipal"].ToString(),
                                CodigoPaisPrincipal = Convert.ToInt32(drAsegurados["CodigoPaisPrincipal"]),
                                DescripcionPaisPrincipal = drAsegurados["DescripcionPaisPrincipal"].ToString(),
                                CodigoDireccionPostal = Convert.ToInt32(drAsegurados["CodigoDireccionPostal"]),
                                DireccionPostal = drAsegurados["DireccionPostal"].ToString(),
                                CodigoPostalPostal = drAsegurados["CodigoPostalPostal"].ToString(),
                                CodigoPaisPostal = Convert.ToInt32(drAsegurados["CodigoPaisPostal"]),
                                DescripcionPaisPostal = drAsegurados["DescripcionPaisPostal"].ToString(),
                                CodigoDireccionAlternativa = Convert.ToInt32(drAsegurados["CodigoDireccionAlternativa"]),
                                DireccionAlternativa = drAsegurados["DireccionAlternativa"].ToString(),
                                CodigoPostalAlternativa = drAsegurados["CodigoPostalAlternativa"].ToString(),
                                CodigoPaisAlternativa = Convert.ToInt32(drAsegurados["CodigoPaisAlternativa"]),
                                DescripcionPaisAlternativa = drAsegurados["DescripcionPaisAlternativa"].ToString()
                           }).ToList();

            oConsultaCertificado.ListaAsegurados = lsAsegurados;

            List<Documento> lsDocumento = new List<Documento>();
            lsDocumento = (from DataRow drDocumentos in dtDocumentos.Rows
                                       select new Documento()
                                       {
                                           CodigoDocumento = Convert.ToInt32(drDocumentos["CodigoDocumento"]),
                                           NombreDocumento = drDocumentos["NombreDocumento"].ToString(),
                                           DescripcionClasificacionTipoDocumento = drDocumentos["DescripcionClasificacionTipoDocumento"].ToString(),
                                           Origen = drDocumentos["Origen"].ToString(),
                                           CodigoTipoDocumento = drDocumentos["CodigoTipoDocumento"].ToString(),
                                           DescripcionTipoDocumento = drDocumentos["DescripcionTipoDocumento"].ToString(),
                                       }).ToList();

            oConsultaCertificado.ListaDocumentos = lsDocumento;

            List<Pagos> lsPagos = new List<Pagos>();
            lsPagos = (from DataRow drPagos in dtPagos.Rows
                           select new Pagos()
                           {
                                CodigoCronogramaPago = Convert.ToInt32(drPagos["CodigoCronogramaPagos"]),
                                MontoCuota = Convert.ToDecimal(drPagos["ValorCuota"]),
                                FechaCobro = Convert.ToDateTime(drPagos["FechaCobro"]),
                                FechaVencimiento = Convert.ToDateTime(drPagos["FechaVencimiento"]),
                                FechaPago =  drPagos["FechaPago"].ToString(),
                                CodigoDocumento = drPagos["CodigoDocumentoPago"].ToString(),
                                CodigoEstadoCuota = drPagos["CodigoEstadoCuota"].ToString(),
                               DescripcionEstadoCuota = drPagos["DescripcionEstadoCuota"].ToString()
                           }).ToList();

            oConsultaCertificado.ListaPagos = lsPagos;

            return View(oConsultaCertificado);
        }

        public ActionResult RegistroPersona(int nCodigoPersona, int nCodigoCertificado)
        {
            RegistroPersona oRegistroPersona = new RegistroPersona();
            if (nCodigoPersona != 0)
            {
                Certificados oCertificados = new Certificados();

                DataTable dtPersona = oCertificados.ObtenerAsegurado(nCodigoPersona, nCodigoCertificado);

                oRegistroPersona.CodigoPersona = Convert.ToInt32(dtPersona.Rows[0]["CodigoPersona"]);
                oRegistroPersona.CodigoPersonaSolicitud = Convert.ToInt32(dtPersona.Rows[0]["CodigoPersona"]);

                oRegistroPersona.CodigoCertificado = nCodigoCertificado;
                oRegistroPersona.CodigoTipoRelacion = dtPersona.Rows[0]["CodigoTipoPersonaCertificado"].ToString();

                oRegistroPersona.Nombre = dtPersona.Rows[0]["Nombre"].ToString();
                oRegistroPersona.ApellidoPaterno = dtPersona.Rows[0]["ApellidoPaterno"].ToString();
                oRegistroPersona.ApellidoMaterno = dtPersona.Rows[0]["ApellidoMaterno"].ToString();
                oRegistroPersona.FechaNacimiento = Convert.ToDateTime(dtPersona.Rows[0]["FechaNacimiento"]);
                oRegistroPersona.Sexo = dtPersona.Rows[0]["Sexo"].ToString();
                oRegistroPersona.CodigoCorreo = Convert.ToInt32(dtPersona.Rows[0]["CodigoCorreo"]);
                oRegistroPersona.Correo = dtPersona.Rows[0]["Correo"].ToString();
                oRegistroPersona.CodigoCelular = Convert.ToInt32(dtPersona.Rows[0]["CodigoCelular"]);
                oRegistroPersona.Celular = dtPersona.Rows[0]["Celular"].ToString();
                oRegistroPersona.CodigoTelefonoCasa = Convert.ToInt32(dtPersona.Rows[0]["CodigoTelefono"]);
                oRegistroPersona.TelefonoCasa = dtPersona.Rows[0]["Telefono"].ToString();
                oRegistroPersona.CodigoDireccionPrincipal = Convert.ToInt32(dtPersona.Rows[0]["CodigoDireccionPrincipal"]);
                oRegistroPersona.DireccionPrincipal = dtPersona.Rows[0]["DireccionPrincipal"].ToString();
                oRegistroPersona.CodigoPaisPrincipal = Convert.ToInt32(dtPersona.Rows[0]["CodigoPaisPrincipal"]);
                oRegistroPersona.CodigoDireccionPostal = Convert.ToInt32(dtPersona.Rows[0]["CodigoDireccionPostal"]);
                oRegistroPersona.DireccionPostal = dtPersona.Rows[0]["DireccionPostal"].ToString();
                oRegistroPersona.CodigoPaisPostal = Convert.ToInt32(dtPersona.Rows[0]["CodigoPaisPostal"]);
                oRegistroPersona.CodigoDireccionAlternativa = Convert.ToInt32(dtPersona.Rows[0]["CodigoDireccionAlternativa"]);
                oRegistroPersona.DireccionAlternativa = dtPersona.Rows[0]["DireccionAlternativa"].ToString();
                oRegistroPersona.CodigoPaisAlternativa = Convert.ToInt32(dtPersona.Rows[0]["CodigoPaisAlternativa"]);
            }
            else
            {

                oRegistroPersona.CodigoPersona = 0;
                oRegistroPersona.CodigoPersonaSolicitud = 0;
                oRegistroPersona.CodigoCertificado = nCodigoCertificado;
                oRegistroPersona.CodigoTipoRelacion = "03";
                oRegistroPersona.Nombre = "";
                oRegistroPersona.ApellidoPaterno = "";
                oRegistroPersona.ApellidoMaterno = "";
                oRegistroPersona.FechaNacimiento = DateTime.Now;
                oRegistroPersona.Sexo = "";
                oRegistroPersona.CodigoCorreo = 0;
                oRegistroPersona.Correo = "";
                oRegistroPersona.CodigoCelular = 0;
                oRegistroPersona.Celular = "";
                oRegistroPersona.CodigoTelefonoCasa = 0;
                oRegistroPersona.TelefonoCasa = "";
                oRegistroPersona.CodigoTelefonoOficina = 0;
                oRegistroPersona.TelefonoOficina = "";
                oRegistroPersona.CodigoDireccionPrincipal = 0;
                oRegistroPersona.DireccionPrincipal = "";
                oRegistroPersona.CodigoPaisPrincipal = 0;
                oRegistroPersona.CodigoDireccionPostal = 0;
                oRegistroPersona.DireccionPostal = "";
                oRegistroPersona.CodigoPaisPostal = 0;
                oRegistroPersona.CodigoDireccionAlternativa = 0;
                oRegistroPersona.DireccionAlternativa = "";
                oRegistroPersona.CodigoPaisAlternativa = 0;
            }


            ConsultasMaestro oConsultasMaestro = new ConsultasMaestro();

            DataTable dtPaises = oConsultasMaestro.ListarPaises();

            List<Paises> lsPaises = new List<Paises>();
            lsPaises = (from DataRow drPaises in dtPaises.Rows
                       select new Paises()
                       {
                          CodigoPais = Convert.ToInt32(drPaises["CodigoPais"]),
                          DescripcionPais  = drPaises["DescripcionPais"].ToString()
                       }).ToList();

            oRegistroPersona.ListaPaises = lsPaises;

            DataTable dtTipoPersonaCertificado = oConsultasMaestro.ListarTipoPersonaCertificado();

            List<TipoPersonaCertificado> lsTipoPersonaCertificado = new List<TipoPersonaCertificado>();
            lsTipoPersonaCertificado = (from DataRow drTipoPersonaCertificado in dtTipoPersonaCertificado.Rows
                        select new TipoPersonaCertificado()
                        {
                            CodigoTipoPersonaCotizacion = drTipoPersonaCertificado["CodigoTipoPersonaCotizacion"].ToString(),
                            DescripcionTipoPersonaCotizacion = drTipoPersonaCertificado["DescripcionTipoPersonaCotizacion"].ToString()
                        }).ToList();

            oRegistroPersona.ListaTipoPersonaCertificado = lsTipoPersonaCertificado;

            return View(oRegistroPersona);
        }

        public ActionResult SubirArchivosCertificado(int nCodigoSolicitud,int nCodigoCertificado)
        {

            Certificados oCertificados = new Certificados();
            ConsultasMaestro oConsultasMaestro = new ConsultasMaestro();

            SubirArchivosCertificado oSubirArchivosCertificado = new SubirArchivosCertificado();
            DataTable dtDocumentos = oCertificados.DocumentosSolicitudClasificado(nCodigoSolicitud);
            DataTable dtCertificado = oCertificados.ObtenerCertificado(nCodigoCertificado);
            DataTable dtAsegurados = oCertificados.ListarAsegurados(nCodigoCertificado);
            DataTable dtTipoDocumentoCliente = oConsultasMaestro.ListarTipoDocumentoCliente();

            oSubirArchivosCertificado.CodigoSolicitud = Convert.ToInt32(dtCertificado.Rows[0]["CodigoSolicitud"]);
            oSubirArchivosCertificado.CodigoCertificado = Convert.ToInt32(dtCertificado.Rows[0]["CodigoCertificado"]);
            oSubirArchivosCertificado.CodigoCotizacion = Convert.ToInt32(dtCertificado.Rows[0]["CodigoCotizacion"]);
            oSubirArchivosCertificado.NumeroPoliza = dtCertificado.Rows[0]["NumeroPoliza"].ToString();
            oSubirArchivosCertificado.CodigoEstadoCertificado = dtCertificado.Rows[0]["CodigoEstadoCertificado"].ToString();
            oSubirArchivosCertificado.DescripcionEstadoCertificado = dtCertificado.Rows[0]["DescripcionEstadoCertificado"].ToString();
            oSubirArchivosCertificado.CodigoEstadoSolicitud = dtCertificado.Rows[0]["CodigoEstadoSolicitud"].ToString();
            oSubirArchivosCertificado.DescripcionEstadoSolicitud = dtCertificado.Rows[0]["DescripcionEstadoSolicitud"].ToString();
            oSubirArchivosCertificado.CodigoPoliza = Convert.ToInt32(dtCertificado.Rows[0]["CodigoPoliza"]);
            oSubirArchivosCertificado.DescripcionPoliza = dtCertificado.Rows[0]["DescripcionPoliza"].ToString();
            oSubirArchivosCertificado.CodigoProducto = Convert.ToInt32(dtCertificado.Rows[0]["CodigoProducto"]);
            oSubirArchivosCertificado.DescripcionProducto = dtCertificado.Rows[0]["DescripcionProducto"].ToString();
            oSubirArchivosCertificado.ColorPrincipal = dtCertificado.Rows[0]["ColorPrincipal"].ToString();
            oSubirArchivosCertificado.Titular = dtAsegurados.Rows[0]["NombreCompleto"].ToString();

            List<Documento> lsDocumento = new List<Documento>();
            lsDocumento = (from DataRow drDocumentos in dtDocumentos.Rows
                           select new Documento()
                           {
                               CodigoDocumento = Convert.ToInt32(drDocumentos["CodigoDocumento"]),
                               NombreDocumento = drDocumentos["NombreDocumento"].ToString(),
                               DescripcionClasificacionTipoDocumento = drDocumentos["DescripcionClasificacionTipoDocumento"].ToString(),
                               Origen = drDocumentos["Origen"].ToString(),
                               CodigoTipoDocumento = drDocumentos["CodigoTipoDocumento"].ToString(),
                               DescripcionTipoDocumento = drDocumentos["DescripcionTipoDocumento"].ToString(),
                           }).ToList();

            oSubirArchivosCertificado.ListaDocumentos = lsDocumento;

            List<TipoDocumentoCliente> lsTipoDocumentoCliente = new List<TipoDocumentoCliente>();
            lsTipoDocumentoCliente = (from DataRow drTipoDocumentoCliente in dtTipoDocumentoCliente.Rows
                           select new TipoDocumentoCliente()
                           {
                               CodigoTipoDocumento = Convert.ToInt32(drTipoDocumentoCliente["CodigoTipoDocumento"]),
                               DescripcionTipoDocumento = drTipoDocumentoCliente["DescripcionTipoDocumento"].ToString(),

                           }).ToList();

            oSubirArchivosCertificado.ListaTipoDocumentoCliente = lsTipoDocumentoCliente;

            return View(oSubirArchivosCertificado);
        }
    }
}