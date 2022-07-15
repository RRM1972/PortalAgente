using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyalIGWEB.Models
{
    public class DashBoardPrincipal
    {
        public int CodigoAgente { get; set; }
        public string NombreAgente { get; set; }
        public string CodigoInternoAgente { get; set; }
        public string NombreCompleto { get; set; }
        public string Cargo { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int TotalCotizaciones { get; set; }
        public int TotalSolicitudesIngresadas { get; set; }
        public int TotalPolizasActivas { get; set; }
        public int PolizasNuevoNegocio { get; set; }
        public int PolizasRenovaciones { get; set; }
        public int PolizasCanceladas { get; set; }
        public int PolizasPrimerPago { get; set; }
        public decimal TotalPrimas { get; set; }
        public decimal PorcentajePrimasPagadas { get; set; }
        public decimal TotalPrimasPagadas { get; set; }
        public decimal PrimasNuevoNegocio { get; set; }
        public decimal PrimasRenovaciones { get; set; }
        public decimal TotalComisiones { get; set; }
        public decimal ComisionesNuevoNegocio { get; set; }
        public decimal ComisionesNuevoNegocioVentaPropia { get; set; }
        public decimal ComisionesNuevoNegocioOtrosAgentes { get; set; }
        public decimal ComisionesRenovaciones { get; set; }
        public decimal ComisionesRenovacionesVentaPropia { get; set; }
        public decimal ComisionesRenovacionesOtrosAgentes { get; set; }
        public List<SolicitudesIngresadas> lsSolicitudesIngresadas { get; set; }
        public List<CotizacionesEstado> lsCotizacionesEstado { get; set; }

    }

    public class SolicitudesIngresadas
    {
        public string CodigoEstado { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Factor { get; set; }
    }

    public class CotizacionesEstado
    {
        public string CodigoEstadoCotizacion { get; set; }
        public string DescripcionEstadoCotizacion { get; set; }
        public int Cantidad { get; set; }

    }

    public class PantallaListadoCotizaciones
    {
        public List<ListadoCotizaciones> lstListadoCotizaciones;
    }
    public class ListadoCotizaciones
    {
        public int CodigoCotizacion { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public string CodigoEstadoCotizacion { get; set; }
        public string DescripcionEstadoCotizacion { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimientoTitular { get; set; }
        public int EdadTitular { get; set; }
        public string FechaNacimientoConyuge { get; set; }
        public string EdadConyuge { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public int Dependientes { get; set; }
        public string Maternidad { get; set; }
        public string Trasplante { get; set; }
        public string SexoTitular { get; set; }
        public string SexoConyuge { get; set; }
        public DateTime FechaHoraCotizacion { get; set; }
        public string DescripcionPais { get; set; }
    }

    public class SolicitarCotizacion
    {
        public int CodigoCotizacion { get; set; }
        public int CodigoAgente { get; set; }
        public DateTime FechaInicioValidez { get; set; }
        public int CodigoPais { get; set; }
        public string NombreSolicitante { get; set; }
        public DateTime FechaNacmimientoSolicitante { get; set; }
        public string SexoSolicitante { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacmimientoConyuge { get; set; }
        public bool IndicadorConyuge { get; set; }
        public string SexoConyuge { get; set; }
        public int NumeroDependientes { get; set; }
        public bool TrasplanteOrganos { get; set; }
        public bool ComplicacionesMaternidad { get; set; }
        public List<Paises> ListarPaises { get; set; }
        public List<Agentes> ListarAgentes { get; set; }
        public List<Dependientes> ListarDependientes { get; set; }

    }

    public class Paises
    {
        public int CodigoPais { get; set; }
        public string DescripcionPais { get; set; }
    }

    public class Agentes
    {
        public int CodigoAgente { get; set; }
        public string NombreAgente { get; set; }
    }
    public class Dependientes
    {
        public int NumeroDependiente { get; set; }
        public string DescripcionNumeroDependiente { get; set; }
    }

    public class ConsultaCotizacion
    {
        public int CodigoCotizacion { get; set; }
        public string CodigoEstadoCotizacion { get; set; }
        public string DescripcionEstadoCotizacion { get; set; }
        public string NombreSolicitante { get; set; }
        public string FechaNacimnientoSolicitante { get; set; }
        public int EdadSolicitante { get; set; }
        public string SexoSolicitante { get; set; }
        public string Correo { get; set; }
        public string DescripcionPais { get; set; }
        public string ComplicacionesMaternidad { get; set; }
        public string TrasplanteOrganos { get; set; }
        public int NumeroDependientes { get; set; }
        public string FechaNacimnientoConyuge { get; set; }
        public int EdadConyuge { get; set; }
        public string SexoConyuge { get; set; }
        public List<PrimasConsulta> ListaPrimasAnualBeyond { get; set; }
        public List<PrimasConsulta> ListaPrimasSemiAnualBeyond { get; set; }
        public List<PrimasConsulta> ListaPrimasTrimestralBeyond { get; set; }
        public List<PrimasConsulta> ListaPrimasAnualPrivilege { get; set; }
        public List<PrimasConsulta> ListaPrimasSemiAnualPrivilege { get; set; }
        public List<PrimasConsulta> ListaPrimasTrimestralPrivilege { get; set; }
        public List<PrimasConsulta> ListaPrimasAnualLiberty { get; set; }
        public List<PrimasConsulta> ListaPrimasSemiAnualLiberty { get; set; }
        public List<PrimasConsulta> ListaPrimasTrimestralLiberty { get; set; }
        public List<PrimasConsulta> ListaPrimasAnualLegacy { get; set; }
        public List<PrimasConsulta> ListaPrimasSemiAnualLegacy { get; set; }
        public List<PrimasConsulta> ListaPrimasTrimestralLegacy { get; set; }
    }

    public class PrimasConsulta
    {
        public string Cobertura { get; set; }
        public string TipoPersona { get; set; }
        public string FormaPago { get; set; }
        public string Opcion1 { get; set; }
        public string Opcion2 { get; set; }
        public string Opcion3 { get; set; }
        public string Opcion4 { get; set; }
        public string Opcion5 { get; set; }
        public string Opcion6 { get; set; }

    }

    public class ListadoPolizasActivas
    {
        public int CodigoCertificado { get; set; }
        public string NumeroPoliza { get; set; }
        public string DescripcionTipoVenta { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroAsegurados { get; set; }
        public string DescripcionPoliza { get; set; }
        public string DescripcionPais { get; set; }
        public string DescripcionEstadoCertificado { get; set; }
        public string FechaInicioVigencia { get; set; }
        public string DescripcionFormaPago { get; set; }
        public decimal Prima { get; set; }
        public string DescripcionPlan { get; set; }
    }

    public class PantallaListadoPolizasActivas
    {
        public List<ListadoPolizasActivas> lsListadoPolizasActivas { get; set; }
    }

    public class Login
    {
        public string user { get; set; }
        public string password { get; set; }
    }

    public class Usuario
    {
        public int CodigoUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int CodigoPerfil { get; set; }
        public string NombrePerfil { get; set; }

    }

    public class DetalleListaComisiones
    {
        public int CodigoCicloComisiones { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NumeroPoliza { get; set; }
        public string NombreCompleto { get; set; }
        public string DescripcionTipoVenta { get; set; }
        public decimal PrimaComisionable { get; set; }
        public decimal PorcentajeComision { get; set; }
        public decimal ValorComision { get; set; }
        public int CodigoAgenteGenera { get; set; }
        public string NombreAgenteGenera { get; set; }
    }

    public class ListarComisionesDashboard
    {
        public List<DetalleListaComisiones> ListaComisiones { get; set; }
    }

    public class DetalleListaAgentesMaster
    {
        public int CodigoAgente { get; set; }
        public string NombreAgente { get; set; }
        public int Polizas { get; set; }
        public decimal Primas { get; set; }
        public decimal Objetivo { get; set; }
        public decimal AvanceObjetivo { get; set; }
        public decimal ComisionPropia { get; set; }
        public decimal ComisionMaster { get; set; }
    }

    public class PerfilAgente
    {
        public Agente DatosAgente { get; set; }
        public List<DetalleListaAgentesMaster> ListaAgentes { get; set; }
        public List<PorcentajeComisionesAgente> ComisionesNuevoNegocio { get; set; }
        public List<PorcentajeComisionesAgente> ComisionesRenovacion { get; set; }
    }

    public class Agente
    {
        public int CodigoAgente { get; set; }
        public int CodigoPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreAgente { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int CodigoAgenteDependencia { get; set; }
        public string NombreAgenteDependencia { get; set; }
        public int CodigoTipoAgente { get; set; }
        public string DescripcionTipoAgente { get; set; }
        public int CodigoNivel { get; set; }
        public int CodigoPais { get; set; }
        public string DescripcionPais { get; set; }
        public string Talla { get; set; }
        public int CodigoCorreo { get; set; }
        public string Correo { get; set; }
        public int CodigoCelular { get; set; }
        public string Celular { get; set; }
        public int CodigoTelefono { get; set; }
        public string Telefono { get; set; }
        public int CodigoTelefonoCasa { get; set; }
        public string TelefonoCasa { get; set; }
        public int CodigoDireccionPersona { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public int CodigoDireccionOficina { get; set; }
        public string DireccionOficina { get; set; }
        public string CiudadOficina { get; set; }
        public string ProvinciaOficina { get; set; }
        public string CodigoPostalOficina { get; set; }
        public int CodigoPaisOficina { get; set; }
        public string DescripcionPaisOficina { get; set; }
        public int CodigoDireccionPostal { get; set; }
        public string DireccionPostal { get; set; }
        public string CiudadPostal { get; set; }
        public string ProvinciaPostal { get; set; }
        public string CodigoPostalPostal { get; set; }
        public int CodigoPaisPostal { get; set; }
        public string DescripcionPaisPostal { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoAgenciaMaster { get; set; }
        public string AgenciaMaster { get; set; }
        public int CodigoBanco { get; set; }
        public string DescripcionBanco { get; set; }
        public string NombreTitularCuenta { get; set; }
        public string NumeroCuentaDeposito { get; set; }
        public string CodigoRouting { get; set; }
        public string DirecciónCuenta { get; set; }
        public string TipoCuentaDeposito { get; set; }

    }

    public class PorcentajeComisionesAgente
    {
        public int CodigoAgente { get; set; }
        public int CodigoAgenteComision { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreAgente { get; set; }
        public int CodigoPoliza { get; set; }
        public string DescripcionPoliza { get; set; }
        public string CodigoTipoVenta { get; set; }
        public string DescripcionTipoVenta { get; set; }
        public decimal PorcentajeComision { get; set; }
    }
}