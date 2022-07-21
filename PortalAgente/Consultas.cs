using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace PortalAgente
{
    class clsBaseDatos
    {
        public class Conexion
        {
            //private string ConnectionStringLoyal = "Data Source=dbloyal.c01l7lhdpql5.us-east-2.rds.amazonaws.com; Initial Catalog=dbLoyal;User=admin;Password=Loyal20210316";
            private string ConnectionStringLoyal = "Data Source=loyaltest.cbpvmf7waxyr.us-east-1.rds.amazonaws.com; Initial Catalog=dbLoyalTest;User=admin;Password=Loyal20210316";
            private string ConnectionStringSeguridad = "Data Source=dbloyal.c01l7lhdpql5.us-east-2.rds.amazonaws.com; Initial Catalog=dbSeguridad;User=admin;Password=Loyal20210316";

            public SqlConnection ObtenerConexion()
            {
                SqlConnection cn = new SqlConnection(ConnectionStringLoyal);
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
                else
                {
                    cn.Open();
                }
                return cn;
            }

            public SqlConnection ObtenerConexionSeguridad()
            {
                SqlConnection cn = new SqlConnection(ConnectionStringSeguridad);
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
                else
                {
                    cn.Open();
                }
                return cn;
            }
        }
    }

    public class Consultas
    {
        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();
        public DataTable SolicitudesEstadoAgente(DateTime dFechaInicio, DateTime dFechaFin, DateTime dFechaInicioComparado, DateTime dFehchaFinComparado, int nCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;


            strSql = "SELECT Solicitudes.CodigoEstadoSolicitud,DescripcionEstadoSolicitud,COUNT(CodigoSolicitud) Cantidad ";
            strSql += "FROM Solicitudes ";
            strSql += "INNER JOIN EstadoSolicitudes ON EstadoSolicitudes.CodigoEstadoSolicitud = Solicitudes.CodigoEstadoSolicitud ";
            strSql += "WHERE CodigoAgente = @CodigoAgente AND FechaSolicitud BETWEEN @FechaInicio AND @FechaFin ";
            strSql += "GROUP BY Solicitudes.CodigoEstadoSolicitud,DescripcionEstadoSolicitud ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;
            da.SelectCommand.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = dFechaInicio;
            da.SelectCommand.Parameters.Add("@FechaFin", SqlDbType.Date).Value = dFechaFin;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable DashBoardPrincipal(DateTime dFechaInicio, DateTime dFechaFin, DateTime dFechaInicioComparado,DateTime dFehchaFinComparado,int nCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("USPDashboardPrincipalAgente", oLocalConnection);
            da.SelectCommand.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = dFechaInicio;
            da.SelectCommand.Parameters.Add("@FechaFin", SqlDbType.Date).Value = dFechaFin;
            da.SelectCommand.Parameters.Add("@FechaInicioComparado", SqlDbType.Date).Value = dFechaInicioComparado;
            da.SelectCommand.Parameters.Add("@FechaFinComparado", SqlDbType.Date).Value = dFehchaFinComparado;
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;

            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable CotizacionesEstadoAgente(DateTime dFechaInicio, DateTime dFechaFin, DateTime dFechaInicioComparado, DateTime dFehchaFinComparado, int nCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT Cotizaciones.CodigoEstadoCotizacion,DescripcionEstadoCotizacion,COUNT(CodigoCotizacion) Cantidad ";
            strSql += "FROM Cotizaciones ";
            strSql += "INNER JOIN EstadoCotizacion ON EstadoCotizacion.CodigoEstadoCotizacion = Cotizaciones.CodigoEstadoCotizacion ";
            strSql += "WHERE CodigoAgente = @CodigoAgente AND FechaCotizacion BETWEEN @FechaInicio AND @FechaFin ";
            strSql += "GROUP BY Cotizaciones.CodigoEstadoCotizacion,DescripcionEstadoCotizacion ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;
            da.SelectCommand.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = dFechaInicio;
            da.SelectCommand.Parameters.Add("@FechaFin", SqlDbType.Date).Value = dFechaFin;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarCotizaciones(int sCodigoAgente, string sCodigoEstadoCotizacion)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoCotizacion, NombreCompleto, FechaNacimientoTitular, EdadTitular, IIF(FechaNacimientoConyuge IS NULL,' ',FORMAT(FechaNacimientoConyuge,'MM/dd/yyyy')) FechaNacimientoConyuge, IIF(EdadConyuge IS NULL,' ',FORMAT(EdadConyuge,'###')) EdadConyuge, CorreoElectronico, Telefono, Dependientes, Maternidad, Trasplante,SexoTitular,IIF(SexoConyuge IS NULL,' ',SexoConyuge) SexoConyuge, ";
            strSql += "FechaCotizacion, FechaHoraCotizacion, CodigoPais, CodigoEstadoCotizacion, DescripcionEstadoCotizacion, DescripcionPais, CodigoAgente, NombreAgente, CorreoAgente, TelefonoAgente ";
            strSql += "FROM VW_Cotizacion ";
            strSql += "WHERE CodigoAgente = @CodigoAgente ";

            if (sCodigoEstadoCotizacion != "")
            { strSql += "AND CodigoEstadoCotizacion = @CodigoEstadoCotizacion "; }

            strSql += "ORDER BY FechaCotizacion DESC ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = sCodigoAgente; 

            if (sCodigoEstadoCotizacion != "")
            { da.SelectCommand.Parameters.Add("@CodigoEstadoCotizacion", SqlDbType.Char).Value = sCodigoEstadoCotizacion.Trim(); }

            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarPolizasActivas(int nCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            string strSQL;

            strSQL = "SELECT CodigoCertificado, NumeroPoliza, DescripcionTipoVenta, NombreCompleto, NumeroAsegurados, DescripcionPoliza, DescripcionPais, ";
            strSQL += "DescripcionEstadoCertificado, FechaInicioVigencia, DescripcionFormaPago, Prima, DescripcionPlan ";
            strSQL += "FROM VW_ListarCertificadosPortalAgente ";
            strSQL += "WHERE CodigoAgente = @CodigoAgente AND CodigoEstadoCertificado IN('03', '07') ";
            strSQL += "ORDER BY NumeroPoliza DESC ";

            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;

            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();



            return dt;
        }

        public DataTable ListarComisiones(int sCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoCicloComisiones, FechaInicio, FechaFin, NumeroPoliza, NombreCompleto, DescripcionTipoVenta, PrimaComisionable, PorcentajeComision, ValorComision, CodigoAgenteGenera, NombreAgenteGenera ";
            strSql += "FROM VW_ListadoComisiones ";
            strSql += "WHERE CodigoAgente = @CodigoAgente AND CodigoCicloComisiones IS NOT NULL ";
            strSql += "ORDER BY CodigoCicloComisiones DESC";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = sCodigoAgente;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarComisionesAgente(int nCodigoAgente, string sCodigoTipoVenta)
        {
            DataTable dt = new DataTable();
            string strSql = "SELECT CodigoAgente,CodigoAgenteComision,NombreCompleto,NombreAgente,CodigoPoliza,DescripcionPoliza,CodigoTipoVenta,DescripcionTipoVenta,PorcentajeComision FROM VW_ComisionesCorredor WHERE CodigoAgente = @CodigoAgente AND CodigoTipoVenta = @CodigoTipoVenta";

            SqlDataAdapter da = new SqlDataAdapter(strSql, oConexion.ObtenerConexion());
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;
            da.SelectCommand.Parameters.Add("@CodigoTipoVenta", SqlDbType.Char).Value = sCodigoTipoVenta;
            da.Fill(dt);
            return dt;
        }

        public DataTable CotizacionConsultaCabecera(int nCodigoCotizacion)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            string strSQL;

            strSQL = "SELECT CodigoCotizacion,FechaInicioVigencia,NombreCompleto,FechaNacimientoTitular,EdadTitular,FechaNacimientoConyuge,EdadConyuge,CorreoElectronico,Telefono,Dependientes,Maternidad,Trasplante,VidaTitular,SumaAseguradaTitular,VidaConyuge,SumaAseguradaConyuge,CodigoAgente,CodigoPais,DescripcionPais,CodigoLenguaje,NombreAgente,CorreoAgente,TelefonoAgente,CodigoPersonaTitular,CodigoPersonaAgente,CodigoEstadoCotizacion,DescripcionEstadoCotizacion,SexoTitular,SexoConyuge ";
            strSQL += "FROM VW_Cotizacion ";
            strSQL += "WHERE CodigoCotizacion = @CodigoCotizacion ";

            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }
        public DataTable SolicitudConsultaAprobada(int nCodigoCotizacion)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            string strSQL;

            strSQL = "SELECT CodigoCotizacion,CodigoSolicitud,NombreCompleto,FechaNacimientoTitular,EdadTitular,FechaNacimientoConyuge,EdadConyuge,CorreoElectronico,Telefono,Dependientes,Maternidad,Trasplante,VidaTitular,SumaAseguradaTitular,VidaConyuge,SumaAseguradaConyuge,CodigoAgente,CodigoPais,DescripcionPais,CodigoLenguaje,NombreAgente,CorreoAgente,TelefonoAgente,DescripcionPoliza,DescripcionPlan,DescripcionFormaPago ";
            strSQL += "FROM VW_CotizacionAprobada ";
            strSQL += "WHERE CodigoCotizacion = @CodigoCotizacion ";

            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }
        public DataTable ListarPrimasSaludPDF(int nCodigoCotizacion, int nCodigoProducto, int nCodigoFormaPago)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            string strSQL;

            DataTable dt = new DataTable();

            strSQL = "SELECT CodigoTipoPersonaCotizacion, CodigoCobertura, PrimasPersonaCotizacion.CodigoPlan,IIF(ValorTexto IS NULL, FORMAT(ValorPrima, '$ ###,###'), ValorTexto) as ValorTexto,IIF(ValorPrima IS NULL, 0, ValorPrima) as ValorPrima ";
            strSQL += "FROM PrimasPersonaCotizacion ";
            strSQL += "INNER JOIN Planes ON Planes.CodigoPlan = PrimasPersonaCotizacion.CodigoPlan ";
            strSQL += "INNER JOIN Polizas ON Polizas.CodigoPoliza = Planes.CodigoPoliza ";
            strSQL += "INNER JOIN PersonaCotizacion ON PersonaCotizacion.CodigoPersona = PrimasPersonaCotizacion.CodigoPersona ";
            strSQL += "WHERE PrimasPersonaCotizacion.CodigoCotizacion = @CodigoCotizacion AND CodigoFormaPago = @CodigoFormaPago AND CodigoProducto = @CodigoProducto ";
            strSQL += "ORDER BY CodigoCobertura,CodigoTipoPersonaCotizacion,CodigoPlan ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.Parameters.Add("@CodigoFormaPago", SqlDbType.Int).Value = nCodigoFormaPago;
            da.SelectCommand.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = nCodigoProducto;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }
        public DataTable ObtenerPrimasSaludPDF(int nCodigoCotizacion, int nCodigoProducto, int nCodigoFormaPago)
        {
            int nNumeroFilas;
            double[] Totales = new double[6];
            string sCodigoTipoPersona = "";
            string sCodigoCobertura = "";
            int nCodigoPlan;
            int nFila = 0;
            int nColumna = 0;

            if (nCodigoFormaPago == 1)
            { nNumeroFilas = 9; }
            else
            { nNumeroFilas = 10; }
            DataTable dtPrimasCotizacion = new DataTable();

            dtPrimasCotizacion.Columns.Add("CodigoCobertura");
            dtPrimasCotizacion.Columns.Add("CodigoTipoPersona");
            dtPrimasCotizacion.Columns.Add("Opcion1");
            dtPrimasCotizacion.Columns.Add("Opcion2");
            dtPrimasCotizacion.Columns.Add("Opcion3");
            dtPrimasCotizacion.Columns.Add("Opcion4");
            dtPrimasCotizacion.Columns.Add("Opcion5");
            dtPrimasCotizacion.Columns.Add("Opcion6");


            for (int i = 0; i < nNumeroFilas; i++)
            {

                dtPrimasCotizacion.Rows.Add();
                dtPrimasCotizacion.Rows[i]["Opcion1"] = "No Aplica";
                dtPrimasCotizacion.Rows[i]["Opcion2"] = "No Aplica";
                dtPrimasCotizacion.Rows[i]["Opcion3"] = "No Aplica";
                dtPrimasCotizacion.Rows[i]["Opcion4"] = "No Aplica";
                dtPrimasCotizacion.Rows[i]["Opcion5"] = "No Aplica";
                dtPrimasCotizacion.Rows[i]["Opcion6"] = "No Aplica";

            }

            DataTable dtPrimasValores = ListarPrimasSaludPDF(nCodigoCotizacion, nCodigoProducto, nCodigoFormaPago);

            int nCodigoPrimerPlan = Convert.ToInt32(dtPrimasValores.Rows[0]["CodigoPlan"]);

            foreach (DataRow drPrimasValores in dtPrimasValores.Rows)
            {
                nCodigoPlan = Convert.ToInt32(drPrimasValores["CodigoPlan"]);

                if (sCodigoTipoPersona != drPrimasValores["CodigoTipoPersonaCotizacion"].ToString() || sCodigoCobertura != drPrimasValores["CodigoCobertura"].ToString())
                {
                    sCodigoTipoPersona = drPrimasValores["CodigoTipoPersonaCotizacion"].ToString();
                    sCodigoCobertura = drPrimasValores["CodigoCobertura"].ToString();
                    nCodigoPlan = Convert.ToInt32(drPrimasValores["CodigoPlan"]);

                    if (sCodigoCobertura == "1")
                    {
                        if (sCodigoTipoPersona == "01")
                        { nFila = 0; }
                        else
                        { nFila = 2; }
                    }

                    if (sCodigoCobertura == "2")
                    {
                        if (sCodigoTipoPersona == "01")
                        { nFila = 1; }
                        else
                        { nFila = 3; }
                    }
                    if (sCodigoCobertura == "3")
                    { nFila = 4; }

                    if (sCodigoCobertura == "4")
                    { nFila = 6; }

                    if (sCodigoCobertura == "5")
                    { nFila = 5; }

                }

                nColumna = (nCodigoPlan - nCodigoPrimerPlan) + 2;

                dtPrimasCotizacion.Rows[nFila]["CodigoCobertura"] = sCodigoCobertura;
                dtPrimasCotizacion.Rows[nFila]["CodigoTipoPersona"] = sCodigoTipoPersona;
                dtPrimasCotizacion.Rows[nFila][nColumna] = drPrimasValores["ValorTexto"].ToString();
                Totales[nColumna - 2] = Totales[nColumna - 2] + Convert.ToDouble(drPrimasValores["ValorPrima"].ToString());


            }

            dtPrimasCotizacion.Rows[7]["CodigoCobertura"] = "CAD";
            dtPrimasCotizacion.Rows[7]["CodigoTipoPersona"] = "";
            dtPrimasCotizacion.Rows[7]["Opcion1"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion2"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion3"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion4"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion5"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion6"] = "$75";

            dtPrimasCotizacion.Rows[8]["CodigoCobertura"] = "TOT";
            dtPrimasCotizacion.Rows[8]["CodigoTipoPersona"] = "";
            dtPrimasCotizacion.Rows[8]["Opcion1"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[0] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion2"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[1] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion3"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[2] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion4"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[3] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion5"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[4] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion6"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[5] + 75));

            if (nNumeroFilas == 10)
            {
                dtPrimasCotizacion.Rows[9]["CodigoCobertura"] = "STO";
                dtPrimasCotizacion.Rows[9]["CodigoTipoPersona"] = "";
                dtPrimasCotizacion.Rows[9]["Opcion1"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[0]));
                dtPrimasCotizacion.Rows[9]["Opcion2"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[1]));
                dtPrimasCotizacion.Rows[9]["Opcion3"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[2]));
                dtPrimasCotizacion.Rows[9]["Opcion4"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[3]));
                dtPrimasCotizacion.Rows[9]["Opcion5"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[4]));
                dtPrimasCotizacion.Rows[9]["Opcion6"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[5]));
            }

            dtPrimasCotizacion.Rows[0]["CodigoCobertura"] = "1";
            dtPrimasCotizacion.Rows[0]["CodigoTipoPersona"] = "01";
            dtPrimasCotizacion.Rows[1]["CodigoCobertura"] = "2";
            dtPrimasCotizacion.Rows[1]["CodigoTipoPersona"] = "01";
            dtPrimasCotizacion.Rows[2]["CodigoCobertura"] = "1";
            dtPrimasCotizacion.Rows[2]["CodigoTipoPersona"] = "02";
            dtPrimasCotizacion.Rows[3]["CodigoCobertura"] = "2";
            dtPrimasCotizacion.Rows[3]["CodigoTipoPersona"] = "02";
            dtPrimasCotizacion.Rows[4]["CodigoCobertura"] = "3";
            dtPrimasCotizacion.Rows[4]["CodigoTipoPersona"] = "";
            dtPrimasCotizacion.Rows[5]["CodigoCobertura"] = "5";
            dtPrimasCotizacion.Rows[5]["CodigoTipoPersona"] = "";
            dtPrimasCotizacion.Rows[6]["CodigoCobertura"] = "4";
            dtPrimasCotizacion.Rows[6]["CodigoTipoPersona"] = "";


            return dtPrimasCotizacion;
        }

        public DataTable ObtenerCertificado(int nCodigoCertificado)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            string strSQL;

            DataTable dt = new DataTable();

            strSQL = "SELECT CodigoSolicitud, CodigoCertificado, NumeroPoliza, CodigoEstadoCertificado, DescripcionEstadoCertificado, CodigoCotizacion, FechaSolicitud, CodigoEstadoSolicitud, DescripcionEstadoSolicitud, ";
            strSQL += "CodigoPlan, DescripcionPlan, DescripcionPlanesConsulta, CodigoPoliza, DescripcionPoliza, CodigoProducto, DescripcionProducto, ColorPrincipal, NumeroAsegurados, ";
            strSQL += "CodigoPais, DescripcionPais, CodigoFormaPago, DescripcionFormaPago, NumeroDependientes, IndicadorTrasplante, IndicadorMaternidad, ";
            strSQL += "FechaAprobacion, CodigoAgente, NombreAgente, FechaInicioVigencia, FechaFinVigencia, Prima, PrimaComisionable, CostoAdministrativo, ";
            strSQL += "CodigoTipoVenta, DescripcionTipoVenta, PeriodoEspera ";
            strSQL += "FROM VW_SolcitudConsulta WHERE CodigoCertificado = @CodigoCertificado ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCertificado", SqlDbType.Int).Value = nCodigoCertificado;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarAgentesMaster(int nCodigoMaster)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT VW_Agentes.CodigoAgente,VW_Agentes.NombreAgente,count(CodigoCertificado) Polizas,IIF(SUM(PrimaComisionable) IS NULL,0,SUM(PrimaComisionable)) Primas,70000 as Objetivo,IIF(SUM(PrimaComisionable) IS NULL,0,(SUM(PrimaComisionable) / 70000)) AvanceObjetivo,IIF(SUM(ComisionPropia.ValorComision) IS NULL,0,SUM(ComisionPropia.ValorComision)) ComisionPropia,IIF(SUM(ComisionMaster.ValorComision) IS NULL,0,SUM(ComisionMaster.ValorComision)) ComisionMaster ";
            strSql += "FROM VW_Agentes ";
            strSql += "LEFT OUTER JOIN VW_Certificados ON VW_Agentes.CodigoAgente = VW_Certificados.CodigoAgente ";
            strSql += "LEFT OUTER JOIN VW_AgentesResumen ComisionPropia ON ComisionPropia.CodigoAgenteGenera = VW_Certificados.CodigoAgente AND ComisionPropia.CodigoAgente = VW_Certificados.CodigoAgente ";
            strSql += "LEFT OUTER JOIN VW_AgentesResumen ComisionMaster ON ComisionMaster.CodigoAgenteGenera = VW_Certificados.CodigoAgente AND ComisionMaster.CodigoAgente = @CodigoAgente ";
            strSql += "WHERE CodigoAgenciaMaster = @CodigoAgente AND VW_Agentes.CodigoAgente <> @CodigoAgente ";
            strSql += "GROUP BY VW_Agentes.CodigoAgente,VW_Agentes.NombreAgente ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoMaster;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ObtenerAgenteDetalle(int nCodigoAgente)
        {
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoAgente, CodigoPersona, TRIM(Nombre) as Nombre, TRIM(ApellidoPaterno) as ApellidoPaterno, TRIM(ApellidoMaterno) as ApellidoMaterno, NombreCompleto,TRIM(NombreAgente) as NombreAgente, Sexo, FechaNacimiento, ";
            strSql = strSql + "Edad, CodigoAgenteDependencia,TRIM(NombreAgenteDependencia) as NombreAgenteDependencia, CodigoTipoAgente, DescripcionTipoAgente, CodigoNivel, CodigoPais,   ";
            strSql = strSql + "DescripcionPais, Estatura, Peso,Talla, ";
            strSql = strSql + "IIF(CodigoCorreo IS NULL, 0, CodigoCorreo) CodigoCorreo,TRIM(Correo) as Correo, ";
            strSql = strSql + "IIF(CodigoCelular IS NULL, 0, CodigoCelular) CodigoCelular, CodigoInternacionalCelular,TRIM(Celular) as Celular, ";
            strSql = strSql + "IIF(CodigoTelefono IS NULL, 0, CodigoTelefono) CodigoTelefono, CodigoInternacionalTelefono,TRIM(Telefono) as Telefono, ";
            strSql = strSql + "IIF(CodigoTelefonoCasa IS NULL, 0, CodigoTelefonoCasa) CodigoTelefonoCasa, TRIM(TelefonoCasa) as TelefonoCasa, ";
            strSql = strSql + "IIF(CodigoDireccionPersona IS NULL, 0, CodigoDireccionPersona) CodigoDireccionPersona, TRIM(Direccion) as Direccion,Ciudad,Provincia,CodigoPostal,  ";
            strSql = strSql + "IIF(CodigoDireccionOficina IS NULL, 0, CodigoDireccionOficina) CodigoDireccionOficina, TRIM(DireccionOficina) as DireccionOficina,CiudadOficina,ProvinciaOficina,CodigoPostalOficina,CodigoPaisOficina,DescripcionPaisOficina, ";
            strSql = strSql + "IIF(CodigoDireccionPostal IS NULL, 0, CodigoDireccionPostal) CodigoDireccionPostal, TRIM(DireccionPostal) as DireccionPostal,CiudadPostal,ProvinciaPostal,CodigoPostalPostal,CodigoPaisPostal,DescripcionPaisPostal, ";
            strSql = strSql + "CodigoUsuario, CodigoAgenciaMaster, AgenciaMaster,  ";
            strSql = strSql + "CodigoBanco,DescripcionBanco,NombreTitularCuenta,NumeroCuentaDeposito,CodigoRouting,DirecciónCuenta,TipoCuentaDeposito ";
            strSql = strSql + "FROM VW_Agentes ";
            strSql = strSql + "WHERE CodigoAgente = @CodigoAgente ";

            SqlDataAdapter da = new SqlDataAdapter(strSql, oConexion.ObtenerConexion());
            if (nCodigoAgente > 0)
            { da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente; }

            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            return dt;
        }

    }

    public class ConsultasMaestro
    {
        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();
        public DataTable ListarPaisesCotizacion()
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoPais, DescripcionPais ";
            strSql += "FROM Paises ";
            strSql += "WHERE CodigoPais<> 45 ";
            strSql += "ORDER BY DescripcionPais ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarTipoDocumentoCliente()
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoTipoDocumento, DescripcionTipoDocumento ";
            strSql += "FROM TipoDocumento ";
            strSql += "WHERE CodigoClasificacionTipoDocumento IN(1, 2, 4) ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarPaises()
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoPais, DescripcionPais ";
            strSql += "FROM Paises ";
            strSql += "ORDER BY DescripcionPais ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }
        public DataTable ListarTipoPersonaCertificado()
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoTipoPersonaCotizacion,DescripcionTipoPersonaCotizacion FROM TipoPersonaCertificado ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ListarAgentesCotizacion(int nCodigoAgente)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoAgente, NombreAgente ";
            strSql += "FROM Agentes ";
            strSql += "WHERE CodigoAgente = @CodigoAgente ";
            strSql += "UNION ";
            strSql += "SELECT CodigoAgente,NombreAgente ";
            strSql += "FROM Agentes ";
            strSql += "WHERE CodigoAgenteDependencia = @CodigoAgente ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = nCodigoAgente;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }


    }

    public class Certificados
    {
        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();

        public DataTable ObtenerCertificado(int nCodigoCertificado)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("USPConsultaSolicitudesGeneral", oLocalConnection);

            da.SelectCommand.Parameters.Add("@CodigoSolicitud", SqlDbType.Int).Value = 0;
            da.SelectCommand.Parameters.Add("@CodigoCertificado", SqlDbType.Int).Value = nCodigoCertificado;
            da.SelectCommand.Parameters.Add("@NumeroPoliza", SqlDbType.Char).Value = 0;


            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;

        }

        public DataTable ListarAsegurados(int nCodigoCertificado)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();
            string strSQL;

            strSQL = "SELECT CodigoSolicitud, CodigoPersonaSolicitud, CodigoPersona, Nombre, ApellidoPaterno, ApellidoMaterno, NombreCompleto, DescripcionTipoPersonaCotizacion, ";
            strSQL += "CodigoSistemaMedida, Talla, Peso, Imc, DescripcionVinculo, FechaNacimiento, Edad, DescripcionEstadoCivil, Sexo, CodigoTipoPersonaCertificado, ";
            strSQL += "IndicadorRestricciones, CodigoTipoPeriodoTiempo, PeriodoEspera, DescripcionPeriodoTiempo, CodigoCertificado, FechaInicioVigencia, FechaFinVigencia, ";
            strSQL += "CodigoCorreo, Correo, CodigoCelular, Celular, CodigoTelefono, Telefono, CodigoDireccionPrincipal, DireccionPrincipal, CodigoPostalPrincipal, ";
            strSQL += "CodigoPaisPrincipal, DescripcionPaisPrincipal, CodigoDireccionPostal, DireccionPostal, CodigoPostalPostal, CodigoPaisPostal, DescripcionPaisPostal, ";
            strSQL += "CodigoDireccionAlternativa, DireccionAlternativa, CodigoPostalAlternativa, CodigoPaisAlternativa, DescripcionPaisAlternativa ";
            strSQL += "FROM VW_Asegurados WHERE CodigoCertificado = @CodigoCertificado ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCertificado", SqlDbType.Int).Value = nCodigoCertificado;

            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable ObtenerAsegurado(int nCodigoPersona,int nCodigoCertificado)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();
            string strSQL;

            strSQL = "SELECT CodigoSolicitud, CodigoPersonaSolicitud, CodigoPersona, Nombre, ApellidoPaterno, ApellidoMaterno, NombreCompleto, DescripcionTipoPersonaCotizacion, ";
            strSQL += "CodigoSistemaMedida, Talla, Peso, Imc, DescripcionVinculo, FechaNacimiento, Edad, DescripcionEstadoCivil, Sexo, CodigoTipoPersonaCertificado, ";
            strSQL += "IndicadorRestricciones, CodigoTipoPeriodoTiempo, PeriodoEspera, DescripcionPeriodoTiempo, CodigoCertificado, FechaInicioVigencia, FechaFinVigencia, ";
            strSQL += "CodigoCorreo, Correo, CodigoCelular, Celular, CodigoTelefono, Telefono, CodigoDireccionPrincipal, DireccionPrincipal, CodigoPostalPrincipal, ";
            strSQL += "CodigoPaisPrincipal, DescripcionPaisPrincipal, CodigoDireccionPostal, DireccionPostal, CodigoPostalPostal, CodigoPaisPostal, DescripcionPaisPostal, ";
            strSQL += "CodigoDireccionAlternativa, DireccionAlternativa, CodigoPostalAlternativa, CodigoPaisAlternativa, DescripcionPaisAlternativa ";
            strSQL += "FROM VW_Asegurados WHERE CodigoPersona = @CodigoPersona AND CodigoCertificado = @CodigoCertificado ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoPersona", SqlDbType.Int).Value = nCodigoPersona;
            da.SelectCommand.Parameters.Add("@CodigoCertificado", SqlDbType.Int).Value = nCodigoCertificado;

            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }

        public DataTable DocumentosSolicitudClasificado(int nCodigoSolicitud)
        {

            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();
            string strSQL;

            strSQL = "SELECT CodigoSolicitud, CodigoDocumento, DescripcionClasificacionTipoDocumento, Origen, CodigoTipoDocumento, DescripcionTipoDocumento, NombreDocumento, FechaRegistro ";
            strSQL = strSQL + "FROM VW_DocumentoSolicitud ";
            strSQL = strSQL + "WHERE CodigoSolicitud = @CodigoSolicitud AND IndicadorActivo = '1' ";
            strSQL = strSQL + "ORDER BY DescripcionClasificacionTipoDocumento,Origen,FechaRegistro ";


            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoSolicitud", SqlDbType.Int).Value = nCodigoSolicitud;

            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();
            return dt;
        }

        public DataTable ListarCuotasSolicitudesConsulta(int nCodigoSolicitud)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();
            string strSQL;

            strSQL = "SELECT CodigoCronogramaPagos, NumeroCuota, ValorCuota, CronogramaPagos.CostoAdministrativo,ValorPrima,ValorIntereses,ValorSaldoCuota,FechaCobro,FechaPago,FechaVencimiento,CronogramaPagos.CodigoEstadoCuota,DescripcionEstadoCuota, ";
            strSQL += "( SELECT TOP 1 CodigoDocumentoPago ";
            strSQL += "FROM PagosCronogramaPagos ";
            strSQL += "INNER JOIN Pagos ON Pagos.CodigoPago = PagosCronogramaPagos.CodigoPago ";
            strSQL += "WHERE PagosCronogramaPagos.CodigoCronogramaPagos = CronogramaPagos.CodigoCronogramaPagos ";
            strSQL += ") as CodigoDocumentoPago ";
            strSQL += "FROM CronogramaPagos ";
            strSQL += "INNER JOIN Certificados ON Certificados.CodigoCertificado = CronogramaPagos.CodigoCertificado ";
            strSQL += "INNER JOIN EstadoCuota ON EstadoCuota.CodigoEstadoCuota = CronogramaPagos.CodigoEstadoCuota ";
            strSQL += "WHERE CodigoSolicitud = @CodigoSolicitud  ";
            strSQL += "ORDER BY FechaVencimiento DESC ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, oLocalConnection);

            da.SelectCommand.Parameters.Add("@CodigoSolicitud", SqlDbType.Int).Value = nCodigoSolicitud;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            return dt;
        }

    }

    public class Cotizaciones
    {
        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();
        string strRutaTemplate = @"C:\inetpub\wwwroot\assets\correotemplates\";
        string strRutaPDF = @"C:\inetpub\wwwroot\assets\PDFTemp\";
        string strRutaTemplatePDF = @"C:\inetpub\wwwroot\assets\templates\";


        public int ObtenerCodigoPDFCotizacion(int nCodigoCotizacion,int nCodigoProducto,int nCodigoUsuario, string sIdioma)
        {
            int nCodigoDocumento;
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoDocumento ";
            strSql += "FROM DocumentoCotizacion ";
            strSql += "WHERE CodigoCotizacion = @CodigoCotizacion AND CodigoProducto = @CodigoProducto ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = nCodigoProducto;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            if(dt.Rows.Count == 0)
            { 
                nCodigoDocumento = PDFCotizacion(nCodigoCotizacion, nCodigoProducto,nCodigoUsuario,sIdioma);
            }
            else
            {
                nCodigoDocumento = Convert.ToInt32(dt.Rows[0]["CodigoDocumento"]);
            }
            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return nCodigoDocumento;
        }


        public int PDFCotizacion(int nCodigoCotizacion, int nCodigoProducto, int nCodigoUsuario, string sIdioma)
        {
            ConsultasTemplate oConsultasTemplate = new ConsultasTemplate();

            string strProducto = "";
            string sPdfTemplate = "";
            string sNombreArchivo = "";
            string sNewFile = "";
            int nCodigoDocumento = 0;

            DataTable dtCotizacion = oConsultasTemplate.ListarCotizacionInforme(nCodigoCotizacion);

            if (nCodigoProducto == 1)
            { strProducto = "BEYOND"; }

            if (nCodigoProducto == 2)
            { strProducto = "PRIVILEGE"; }

            if (nCodigoProducto == 3)
            { strProducto = "LIBERTY"; }

            if (nCodigoProducto == 4)
            { strProducto = "LEGACY"; }

            if (!Directory.Exists(strRutaPDF))
            {
                DirectoryInfo di = Directory.CreateDirectory(strRutaPDF);
            }
            sNombreArchivo = "LOYAL - Cotizacion Nro." + dtCotizacion.Rows[0]["CodigoCotizacion"].ToString().Trim() + " - " + dtCotizacion.Rows[0]["NombreCompleto"].ToString().Trim() + "-" + strProducto + ".pdf";
            //sPdfTemplate = strRutaTemplate + "FormatoCotizacion" + strProducto + ".pdf";
            sPdfTemplate = strRutaTemplatePDF + "FormatoPrimas" + strProducto.Trim() + "2021" + sIdioma + ".pdf";
            sNewFile = strRutaPDF + sNombreArchivo;

            PdfReader oPdfReader = new PdfReader(sPdfTemplate);
            AcroFields af = oPdfReader.AcroFields;

            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper oPdfStamper = new PdfStamper(oPdfReader, ms, '\0', false))
                {
                    AcroFields oPdfFormFields = oPdfStamper.AcroFields; // do stuff
                                                                        //CodigoCotizacion, NombreCompleto, FechaNacimientoTitular, EdadTitular, FechaNacimientoConyuge, EdadConyuge, CorreoElectronico, Telefono, Dependientes, Maternidad, Trasplante, VidaTitular, SumaAseguradaTitular, VidaConyuge, SumaAseguradaConyuge, CodigoAgente, CodigoPais, DescripcionPais, CodigoLenguaje, NombreAgente, CorreoAgente, TelefonoAgente
                    if (sIdioma == "es")
                    {
                        oPdfFormFields.SetField("NumeroCotizacion", "Cotización Nro." + dtCotizacion.Rows[0]["CodigoCotizacion"].ToString().Trim());
                        oPdfFormFields.SetField("NombreTitular", dtCotizacion.Rows[0]["NombreCompleto"].ToString().Trim() + " - " + dtCotizacion.Rows[0]["FechaNacimientoTitular"].ToString().Trim() + " ( " + dtCotizacion.Rows[0]["EdadTitular"].ToString().Trim() + " años )");
                        if (dtCotizacion.Rows[0]["EdadConyuge"].ToString().Trim() == "")
                        { oPdfFormFields.SetField("EdadConyuge", "No Aplica"); }
                        else
                        { oPdfFormFields.SetField("EdadConyuge", dtCotizacion.Rows[0]["EdadConyuge"].ToString().Trim() + " años"); }

                    }

                    if (sIdioma == "en")
                    {
                        oPdfFormFields.SetField("NumeroCotizacion", "Quote No." + dtCotizacion.Rows[0]["CodigoCotizacion"].ToString().Trim());
                        oPdfFormFields.SetField("NombreTitular", dtCotizacion.Rows[0]["NombreCompleto"].ToString().Trim() + " - " + dtCotizacion.Rows[0]["FechaNacimientoTitular"].ToString().Trim() + " ( " + dtCotizacion.Rows[0]["EdadTitular"].ToString().Trim() + " years )");
                        if (dtCotizacion.Rows[0]["EdadConyuge"].ToString().Trim() == "")
                        { oPdfFormFields.SetField("EdadConyuge", "not apply"); }
                        else
                        { oPdfFormFields.SetField("EdadConyuge", dtCotizacion.Rows[0]["EdadConyuge"].ToString().Trim() + " years"); }

                    }

                    oPdfFormFields.SetField("NombrePais", dtCotizacion.Rows[0]["DescripcionPais"].ToString().Trim());
                    oPdfFormFields.SetField("SumaAseguradaTitular", dtCotizacion.Rows[0]["SumaAseguradaTitular"].ToString().Trim());
                    oPdfFormFields.SetField("Trasplante", dtCotizacion.Rows[0]["Trasplante"].ToString().Trim());
                    oPdfFormFields.SetField("Maternidad", dtCotizacion.Rows[0]["Maternidad"].ToString().Trim());
                    oPdfFormFields.SetField("Hijos", dtCotizacion.Rows[0]["Dependientes"].ToString().Trim());
                    oPdfFormFields.SetField("SumaAseguradaConyuge", dtCotizacion.Rows[0]["SumaAseguradaConyuge"].ToString().Trim());
                    oPdfFormFields.SetField("NombreAgente", dtCotizacion.Rows[0]["NombreAgente"].ToString().Trim());
                    oPdfFormFields.SetField("CorreoAgente", dtCotizacion.Rows[0]["CorreoAgente"].ToString().Trim());
                    oPdfFormFields.SetField("telefonoAgente", dtCotizacion.Rows[0]["TelefonoAgente"].ToString().Trim());


                    string strColPrima = "";
                    decimal nValorPrima = 0;
                    string sPrima = "";

                    DataTable dtPrimaAnual = oConsultasTemplate.ObtenerPrimasSaludPDF(nCodigoCotizacion, nCodigoProducto, 1);

                    for (int nFila = 1; nFila <= 9; nFila++)
                    {
                        for (int nCol = 1; nCol <= 6; nCol++)
                        {
                            strColPrima = "Prima01" + ("0" + nFila.ToString().Trim()).Substring(("0" + nFila.ToString().Trim()).Length - 2, 2) + ("0" + nCol.ToString().Trim()).Substring(("0" + nCol.ToString().Trim()).Length - 2, 2);
                            //nValorPrima = Convert.ToDecimal(dtPrimaAnual.Rows[nFila - 1][nCol + 7]);
                            sPrima = dtPrimaAnual.Rows[nFila - 1][nCol - 1].ToString();
                            if (sPrima == "No Aplica")
                            {
                                if (sIdioma == "en")
                                { sPrima = "not apply"; }
                            }

                            oPdfFormFields.SetField(strColPrima, sPrima);
                        }
                    }


                    nValorPrima = 0;
                    DataTable dtPrimaSemestral = oConsultasTemplate.ObtenerPrimasSaludPDF(nCodigoCotizacion, nCodigoProducto, 2);

                    for (int nFila = 1; nFila <= 10; nFila++)
                    {
                        for (int nCol = 1; nCol <= 6; nCol++)
                        {
                            strColPrima = "Prima02" + ("0" + nFila.ToString().Trim()).Substring(("0" + nFila.ToString().Trim()).Length - 2, 2) + ("0" + nCol.ToString().Trim()).Substring(("0" + nCol.ToString().Trim()).Length - 2, 2);
                            //nValorPrima = Convert.ToDecimal(dtPrimaSemestral.Rows[nFila - 1][nCol + 7]);
                            sPrima = dtPrimaSemestral.Rows[nFila - 1][nCol - 1].ToString();
                            if (sPrima == "No Aplica")
                            {
                                if (sIdioma == "en")
                                { sPrima = "not apply"; }
                            }

                            oPdfFormFields.SetField(strColPrima, sPrima);
                        }
                    }

                    nValorPrima = 0;
                    DataTable dtPrimaTrimestral = oConsultasTemplate.ObtenerPrimasSaludPDF(nCodigoCotizacion, nCodigoProducto, 3);

                    for (int nFila = 1; nFila <= 10; nFila++)
                    {
                        for (int nCol = 1; nCol <= 6; nCol++)
                        {
                            strColPrima = "Prima03" + ("0" + nFila.ToString().Trim()).Substring(("0" + nFila.ToString().Trim()).Length - 2, 2) + ("0" + nCol.ToString().Trim()).Substring(("0" + nCol.ToString().Trim()).Length - 2, 2);
                            //nValorPrima = Convert.ToDecimal(dtPrimaTrimestral.Rows[nFila - 1][nCol + 7]);
                            sPrima = dtPrimaTrimestral.Rows[nFila - 1][nCol - 1].ToString();
                            if (sPrima == "No Aplica")
                            {
                                if (sIdioma == "en")
                                { sPrima = "not apply"; }
                            }

                            oPdfFormFields.SetField(strColPrima, sPrima);
                        }
                    }

                    oPdfStamper.FormFlattening = true;
                    oPdfStamper.Close();
                    oPdfReader.Close();
                    // 
                }
                
                Documentos oDocumentos = new Documentos();

                DocumentosProceso oClsDocumentos = new DocumentosProceso();

                oDocumentos.NombreDocumento = sNombreArchivo;
                oDocumentos.Documento = ms.ToArray();
                oDocumentos.UsuarioRegistro = nCodigoUsuario;

                nCodigoDocumento = oClsDocumentos.GrabarDocumento(oDocumentos);

                oClsDocumentos.GrabarDocumentoCotizacion(nCodigoCotizacion, nCodigoDocumento, nCodigoProducto, nCodigoUsuario);
            }

            return nCodigoDocumento;
        }



        public int GenerarCotizacionSalud(FormularioCotizacion oFormularioCotizacion)
        {
            SqlDataReader oCodigoCotizacion;

            SqlDataAdapter da = new SqlDataAdapter("USPCotizacionSaludV2", oConexion.ObtenerConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = oFormularioCotizacion.CodigoCotizacion;
            da.SelectCommand.Parameters.Add("@FechaCotizacion", SqlDbType.DateTime).Value = oFormularioCotizacion.FechaCotizacion;
            da.SelectCommand.Parameters.Add("@FechaInicioVigencia", SqlDbType.DateTime).Value = oFormularioCotizacion.FechaInicioVigencia;
            da.SelectCommand.Parameters.Add("@Nombre", SqlDbType.Char).Value = oFormularioCotizacion.Nombre;
            da.SelectCommand.Parameters.Add("@ApellidoPaterno", SqlDbType.Char).Value = oFormularioCotizacion.ApellidoPaterno;
            da.SelectCommand.Parameters.Add("@ApellidoMaterno", SqlDbType.Char).Value = oFormularioCotizacion.ApellidoMaterno;
            da.SelectCommand.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime).Value = oFormularioCotizacion.FechaNacimiento;
            da.SelectCommand.Parameters.Add("@Pais", SqlDbType.SmallInt).Value = oFormularioCotizacion.Pais;
            da.SelectCommand.Parameters.Add("@SexoContratante", SqlDbType.Char).Value = oFormularioCotizacion.SexoContratante;
            if (oFormularioCotizacion.IndicadorConyuge)
            {
                da.SelectCommand.Parameters.Add("@FechaNacimientoConyuge", SqlDbType.DateTime).Value = oFormularioCotizacion.FechaNacimientoConyuge;
                da.SelectCommand.Parameters.Add("@SexoConyuge", SqlDbType.Char).Value = oFormularioCotizacion.SexoConyuge;
            }
            else
            {
                da.SelectCommand.Parameters.Add("@FechaNacimientoConyuge", SqlDbType.DateTime).Value = DBNull.Value;
                da.SelectCommand.Parameters.Add("@SexoConyuge", SqlDbType.Char).Value = DBNull.Value;
            }


            da.SelectCommand.Parameters.Add("@Dependientes", SqlDbType.TinyInt).Value = oFormularioCotizacion.Dependientes;
            da.SelectCommand.Parameters.Add("@Correo", SqlDbType.Char).Value = oFormularioCotizacion.Correo;
            da.SelectCommand.Parameters.Add("@Trasplante", SqlDbType.Char).Value = oFormularioCotizacion.Trasplante;
            da.SelectCommand.Parameters.Add("@Maternidad", SqlDbType.Char).Value = oFormularioCotizacion.Maternidad;
            da.SelectCommand.Parameters.Add("@SumaAseguradaPrincipal", SqlDbType.Decimal).Value = oFormularioCotizacion.SumaAseguradaPrincipal;
            da.SelectCommand.Parameters.Add("@SumaAseguradaConyuge", SqlDbType.Decimal).Value = oFormularioCotizacion.SumaAseguradaConyuge;
            da.SelectCommand.Parameters.Add("@CodigoAgente", SqlDbType.Int).Value = oFormularioCotizacion.CodigoAgente;
            da.SelectCommand.Parameters.Add("@CodigoUsuario", SqlDbType.Int).Value = oFormularioCotizacion.CodigoUsuario;


            oCodigoCotizacion = da.SelectCommand.ExecuteReader();

            oCodigoCotizacion.Read();

            return Convert.ToInt32(oCodigoCotizacion[0].ToString());
        }

    }

    public class DocumentosProceso
    {
        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();
        public int GrabarDocumento(Documentos oDocumentos)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            SqlDataReader oCodigDocumento;
            int nCodigoDocumento;

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("USPCreaDocumentos", oLocalConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;


            da.SelectCommand.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = oDocumentos.NombreDocumento;
            da.SelectCommand.Parameters.Add("@Documento", SqlDbType.VarBinary).Value = oDocumentos.Documento;
            da.SelectCommand.Parameters.Add("@UsuarioRegistro", SqlDbType.Int).Value = oDocumentos.UsuarioRegistro;

            oCodigDocumento = da.SelectCommand.ExecuteReader();

            oCodigDocumento.Read();

            nCodigoDocumento = Convert.ToInt32(oCodigDocumento[0]);

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return nCodigoDocumento;
        }

        public bool GrabarDocumentoEntidad(int nCodigoTipoEntidad, int nCodigoIdEntidad, int nCodigoDocumento, int nCodigoTipoDocumento, string sOtroTipoDocumento, int nUsuarioRegistro)
        {
            SqlConnection oLocalConnection = new SqlConnection();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("USPCreaDocumentoEntidad", oLocalConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@CodigoTipoEntidad", SqlDbType.Int).Value = nCodigoTipoEntidad;
            da.SelectCommand.Parameters.Add("@CodigoIdEntidad", SqlDbType.Int).Value = nCodigoIdEntidad;
            da.SelectCommand.Parameters.Add("@CodigoDocumento", SqlDbType.Int).Value = nCodigoDocumento;
            da.SelectCommand.Parameters.Add("@CodigoTipoDocumento", SqlDbType.Int).Value = nCodigoTipoDocumento;
            da.SelectCommand.Parameters.Add("@OtroTipoDocumento", SqlDbType.VarChar).Value = sOtroTipoDocumento;
            da.SelectCommand.Parameters.Add("@UsuarioRegistro", SqlDbType.Int).Value = nUsuarioRegistro;

            da.SelectCommand.ExecuteNonQuery();

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return true;
        }

        public bool GrabarDocumentoCotizacion(int nCodigoCotizacion, int nCodigoDocumento, int nCodigoProducto, int nCodigoUsuarioRegistro)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            SqlDataReader oCodigoCotizacion;

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("INSERT INTO DocumentoCotizacion (CodigoCotizacion,CodigoDocumento,CodigoTipoDocumento,CodigoProducto,UsuarioRegistro) VALUES (@CodigoCotizacion,@CodigoDocumento,8,@CodigoProducto,@UsuarioRegistro)", oLocalConnection);
            da.SelectCommand.CommandType = CommandType.Text;

            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.Parameters.Add("@CodigoDocumento", SqlDbType.Int).Value = nCodigoDocumento;
            da.SelectCommand.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = nCodigoProducto;
            da.SelectCommand.Parameters.Add("@UsuarioRegistro", SqlDbType.Int).Value = nCodigoUsuarioRegistro;

            oCodigoCotizacion = da.SelectCommand.ExecuteReader();

            oCodigoCotizacion.Read();

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return true;
        }

        public DataTable ObtenerDocumento(int nCodigoDocumento)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("SELECT CodigoDocumento,Documento,NombreDocumento FROM Documentos WHERE CodigoDocumento = @CodigoDocumento", oLocalConnection);
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.SelectCommand.Parameters.Add("@CodigoDocumento", SqlDbType.Int).Value = nCodigoDocumento;
            da.Fill(dt);

            oLocalConnection.Close();
            da.Dispose();

            return dt;
        }


    }
    public class Documentos
    {
        public int CodigoDocumento { set; get; }
        public string NombreDocumento { set; get; }
        public byte[] Documento { set; get; }
        public int UsuarioRegistro { set; get; }
    }

    public class FormularioCotizacion
    {
        public int CodigoCotizacion { set; get; }
        public DateTime FechaCotizacion { set; get; }
        public DateTime FechaInicioVigencia { set; get; }
        public string Nombre { set; get; }
        public string ApellidoPaterno { set; get; }
        public string ApellidoMaterno { set; get; }
        public DateTime FechaNacimiento { set; get; }
        public int Pais { set; get; }
        public string SexoContratante { set; get; }
        public bool IndicadorConyuge { set; get; }
        public DateTime FechaNacimientoConyuge { set; get; }
        public string SexoConyuge { set; get; }
        public int Dependientes { set; get; }
        public string Correo { set; get; }
        public string Trasplante { set; get; }
        public string Maternidad { set; get; }
        public decimal SumaAseguradaPrincipal { set; get; }
        public decimal SumaAseguradaConyuge { set; get; }
        public int CodigoAgente { set; get; }
        public int CodigoUsuario { set; get; }

    }

}
