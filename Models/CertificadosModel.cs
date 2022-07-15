using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyalIGWEB.Models
{
    public class ConsultaCertificado
    {
        public int CodigoSolicitud { get; set; }
        public int CodigoCertificado { get; set; }
        public int CodigoCotizacion { get; set; }
        public string NumeroPoliza { get; set; }
        public string CodigoEstadoCertificado { get; set; }
        public string DescripcionEstadoCertificado { get; set; }
        public string CodigoEstadoSolicitud { get; set; }
        public string DescripcionEstadoSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public int CodigoPlan { get; set; }
        public string DescripcionPlan { get; set; }
        public string DescripcionPlanesConsulta { get; set; }
        public int CodigoPoliza { get; set; }
        public string DescripcionPoliza { get; set; }
        public int CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string ColorPrincipal { get; set; }
        public int NumeroAsegurados { get; set; }
        public int CodigoPais { get; set; }
        public string DescripcionPais { get; set; }
        public int CodigoFormaPago { get; set; }
        public string DescripcionFormaPago { get; set; }
        public int NumeroDependientes { get; set; }
        public string IndicadorTrasplante { get; set; }
        public string IndicadorMaternidad { get; set; }
        public int CodigoAgente { get; set; }
        public string NombreAgente { get; set; }
        public decimal Prima { get; set; }
        public decimal PrimaComisionable { get; set; }
        public decimal CostoAdministrativo { get; set; }
        public string CodigoTipoVenta { get; set; }
        public string DescripcionTipoVenta { get; set; }
        public int PeriodoEspera { get; set; }

        public List<Documento> ListaDocumentos { get; set; }
        public List<Asegurados> ListaAsegurados { get; set; }
        public List<Pagos> ListaPagos { get; set; }
    }

    public class Documento
    {
        public int CodigoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string DescripcionClasificacionTipoDocumento { get; set; }
        public string Origen { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string DescripcionTipoDocumento { get; set; }
    }

    public class Asegurados
    {
        public int CodigoSolicitud { get; set; }
        public int CodigoPersonaSolicitud { get; set; }
        public int CodigoPersona { get; set; }
        public int CodigoCertificado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string DescripcionTipoPersonaCotizacion { get; set; }
        public string CodigoSistemaMedida { get; set; }
        public decimal Talla { get; set; }
        public decimal Peso { get; set; }
        public decimal Imc { get; set; }
        public string DescripcionVinculo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string DescripcionEstadoCivil { get; set; }
        public string Sexo { get; set; }
        public string CodigoTipoPersonaCertificado { get; set; }
        public string IndicadorRestricciones { get; set; }
        public string CodigoTipoPeriodoTiempo { get; set; }
        public int PeriodoEspera { get; set; }
        public string DescripcionPeriodoTiempo { get; set; }
        public string FechaInicioVigencia  { get; set; }
        public string FechaFinVigencia  { get; set; }
        public int CodigoCorreo { get; set; }
        public string Correo { get; set; }
        public int CodigoCelular { get; set; }
        public string Celular { get; set; }
        public int CodigoTelefono { get; set; }
        public string Telefono { get; set; }
        public int CodigoDireccionPrincipal { get; set; }
        public string DireccionPrincipal { get; set; }
        public string CodigoPostalPrincipal { get; set; }
        public int CodigoPaisPrincipal { get; set; }
        public string DescripcionPaisPrincipal { get; set; }
        public int CodigoDireccionPostal { get; set; }
        public string DireccionPostal { get; set; }
        public string CodigoPostalPostal { get; set; }
        public int CodigoPaisPostal { get; set; }
        public string DescripcionPaisPostal { get; set; }
        public int CodigoDireccionAlternativa { get; set; }
        public string DireccionAlternativa { get; set; }
        public string CodigoPostalAlternativa { get; set; }
        public int CodigoPaisAlternativa { get; set; }
        public string DescripcionPaisAlternativa { get; set; }
    }

    public class Pagos
    {
        public int CodigoCronogramaPago { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime FechaCobro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string FechaPago { get; set; }
        public string CodigoDocumento { get; set; }
        public string CodigoEstadoCuota { get; set; }
        public string DescripcionEstadoCuota { get; set; }
    }

    public class RegistroPersona
    {
        public int CodigoPersona { get; set; }
        public int CodigoPersonaSolicitud { get; set; }
        public int CodigoCertificado { get; set; }
        public string CodigoTipoRelacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public int CodigoCorreo { get; set; }
        public string Correo { get; set; }
        public int CodigoCelular { get; set; }
        public string Celular { get; set; }
        public int CodigoTelefonoCasa { get; set; }
        public string TelefonoCasa { get; set; }
        public int CodigoTelefonoOficina { get; set; }
        public string TelefonoOficina { get; set; }
        public int CodigoDireccionPrincipal { get; set; }
        public string DireccionPrincipal { get; set; }
        public int CodigoPaisPrincipal { get; set; }
        public int CodigoDireccionPostal { get; set; }
        public string DireccionPostal { get; set; }
        public int CodigoPaisPostal { get; set; }
        public int CodigoDireccionAlternativa { get; set; }
        public string DireccionAlternativa { get; set; }
        public int CodigoPaisAlternativa { get; set; }
        public List<Paises> ListaPaises { get; set; }
        public List<TipoPersonaCertificado>  ListaTipoPersonaCertificado { get; set; }
}

    public class TipoPersonaCertificado
    {
        public string CodigoTipoPersonaCotizacion { get; set; }
        public string DescripcionTipoPersonaCotizacion { get; set; }
    }
    public class TipoDocumentoCliente
    {
        public int CodigoTipoDocumento { get; set; }
        public string DescripcionTipoDocumento { get; set; }
    }

    public class SubirArchivosCertificado
    {
        public int CodigoSolicitud { get; set; }
        public int CodigoCertificado { get; set; }
        public int CodigoCotizacion { get; set; }
        public string NumeroPoliza { get; set; }
        public string CodigoEstadoCertificado { get; set; }
        public string DescripcionEstadoCertificado { get; set; }
        public string CodigoEstadoSolicitud { get; set; }
        public string DescripcionEstadoSolicitud { get; set; }
        public int CodigoPoliza { get; set; }
        public string DescripcionPoliza { get; set; }
        public int CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string ColorPrincipal { get; set; }
        public string Titular { get; set; }
        public int CodigoTipoDocumento { get; set; }
        public List<TipoDocumentoCliente> ListaTipoDocumentoCliente { get; set; }
        public List<Documento> ListaDocumentos { get; set; }
    }

}