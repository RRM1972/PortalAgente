using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PortalAgente
{
    public class ProcesoCorreo
    {
        public bool EnviarCorreo(Correo oCorreo)
        {
            MailMessage oMail = new MailMessage();

            oMail.From = new MailAddress(oCorreo.De);

            oMail.To.Add(oCorreo.Para);

            if (oCorreo.Copia != null)
            {
                if (oCorreo.Copia == "")
                { oMail.CC.Add("it@loyalig.com"); }
                else
                { oMail.CC.Add(oCorreo.Copia); }
            }
            oMail.Sender = new MailAddress(oCorreo.De);
            oMail.Subject = oCorreo.Asunto;
            oMail.Body = oCorreo.Texto;
            oMail.IsBodyHtml = true;

            if (oCorreo.RutaArchivo != null)
            {
                if (oCorreo.RutaArchivo.Length > 0)
                {
                    for (int i = 0; i < oCorreo.RutaArchivo.Length; i++)
                    {
                        if (oCorreo.RutaArchivo[i] != "")
                        {
                            oMail.Attachments.Add(new Attachment(oCorreo.RutaArchivo[i]));
                        }
                    }
                }
            }
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(oCorreo.De, oCorreo.Password);
            smtp.EnableSsl = true;
            smtp.Timeout = 200000;
            smtp.Send(oMail);


            oMail.Dispose();

            foreach(string strArchivo in oCorreo.RutaArchivo)
            {
                if (strArchivo.Substring(0, 12) == @"C:\TempLoyal")
                { File.Delete(strArchivo); }
            }
            
            return true;
        }
    }

    public class Correo
    {
        public string NombreDe { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Texto { get; set; }
        public string Password { get; set; }
        public string Copia { get; set; }
        public string[] RutaArchivo { get; set; }

    }

    public class ConsultasTemplate
    {

        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();
        public DataTable CorreoCotizacion(int nCodigoCotizacion, int nCodigoProducto)
        {
            SqlConnection oLocalConnection = new SqlConnection();

            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT Cotizaciones.CodigoCotizacion,Personas.NombreCompleto,Cotizaciones.CorreoElectronico,CodigoDocumento ";
            strSql += "FROM Cotizaciones ";
            strSql += "INNER JOIN PersonaCotizacion ON PersonaCotizacion.CodigoCotizacion = Cotizaciones.CodigoCotizacion AND PersonaCotizacion.CodigoTipoPersonaCotizacion = '01' ";
            strSql += "INNER JOIN Personas ON Personas.CodigoPersona = PersonaCotizacion.CodigoPersona ";
            strSql += "LEFT OUTER JOIN DocumentoCotizacion ON DocumentoCotizacion.CodigoCotizacion = Cotizaciones.CodigoCotizacion ";
            strSql += "WHERE Cotizaciones.CodigoCotizacion = @CodigoCotizacion AND (CodigoProducto = @CodigoProducto OR CodigoProducto Is NULL) ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = nCodigoProducto;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return dt;
        }
        public DataTable ListarCotizacionInforme(int nCodigoCotizacion)
        {
            SqlConnection oLocalConnection = new SqlConnection();

            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoCotizacion, NombreCompleto, dbo.fncFecha(FechaNacimientoTitular, '1') as FechaNacimientoTitular , EdadTitular, dbo.fncFecha(FechaNacimientoConyuge, '1') as FechaNacimientoConyuge, ";
            strSql += "EdadConyuge, CorreoElectronico, Telefono, Dependientes, IIF(Maternidad = 1, 'SI', 'NO') as Maternidad, IIF(Trasplante = 1, 'SI', 'NO') as Trasplante, VidaTitular, ";
            strSql += "FORMAT(SumaAseguradaTitular, '$ ###,###') as SumaAseguradaTitular,VidaConyuge, FORMAT(SumaAseguradaConyuge, '$ ###,###') SumaAseguradaConyuge, CodigoAgente, CodigoPais, DescripcionPais, CodigoLenguaje, NombreAgente, CorreoAgente, TelefonoAgente ";
            strSql += "FROM VW_Cotizacion ";
            strSql += "WHERE CodigoCotizacion = @CodigoCotizacion ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return dt;
        }
        public DataTable ObtenerPrimasSaludPDF(int nCodigoCotizacion, int nCodigoProducto, int nCodigoFormaPago)
        {
            int nNumeroFilas;
            double[] Totales = new double[6];
            string sCodigoTipoPersona = "";
            string sCodigoCobertura = "";
            int nFila = 0;
            int nColumna = 0;

            if (nCodigoFormaPago == 1)
            { nNumeroFilas = 9; }
            else
            { nNumeroFilas = 10; }
            DataTable dtPrimasCotizacion = new DataTable();

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


            foreach (DataRow drPrimasValores in dtPrimasValores.Rows)
            {
                if (sCodigoTipoPersona != drPrimasValores["CodigoTipoPersonaCotizacion"].ToString() || sCodigoCobertura != drPrimasValores["CodigoCobertura"].ToString())
                {
                    sCodigoTipoPersona = drPrimasValores["CodigoTipoPersonaCotizacion"].ToString();
                    sCodigoCobertura = drPrimasValores["CodigoCobertura"].ToString();

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

                    nColumna = 0;
                }

                dtPrimasCotizacion.Rows[nFila][nColumna] = drPrimasValores["ValorTexto"].ToString();
                Totales[nColumna] = Totales[nColumna] + Convert.ToDouble(drPrimasValores["ValorPrima"].ToString());

                nColumna++;

            }

            dtPrimasCotizacion.Rows[7]["Opcion1"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion2"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion3"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion4"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion5"] = "$75";
            dtPrimasCotizacion.Rows[7]["Opcion6"] = "$75";

            dtPrimasCotizacion.Rows[8]["Opcion1"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[0] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion2"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[1] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion3"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[2] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion4"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[3] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion5"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[4] + 75));
            dtPrimasCotizacion.Rows[8]["Opcion6"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[5] + 75));

            if (nNumeroFilas == 10)
            {
                dtPrimasCotizacion.Rows[9]["Opcion1"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[0]));
                dtPrimasCotizacion.Rows[9]["Opcion2"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[1]));
                dtPrimasCotizacion.Rows[9]["Opcion3"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[2]));
                dtPrimasCotizacion.Rows[9]["Opcion4"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[3]));
                dtPrimasCotizacion.Rows[9]["Opcion5"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[4]));
                dtPrimasCotizacion.Rows[9]["Opcion6"] = string.Format("$ {0:###,###,###}", Convert.ToDecimal(Totales[5]));
            }

            return dtPrimasCotizacion;
        }

        public DataTable ListarPrimasSaludPDF(int nCodigoCotizacion, int nCodigoProducto, int nCodigoFormaPago)
        {
            SqlConnection oLocalConnection = new SqlConnection();
            DataTable dt = new DataTable();

            string strSql;

            strSql = "SELECT CodigoTipoPersonaCotizacion, CodigoCobertura, PrimasPersonaCotizacion.CodigoPlan,IIF(ValorTexto IS NULL, FORMAT(ValorPrima, '$ ###,###'), ValorTexto) as ValorTexto,IIF(ValorPrima IS NULL, 0, ValorPrima) as ValorPrima ";
            strSql += "FROM PrimasPersonaCotizacion ";
            strSql += "INNER JOIN Planes ON Planes.CodigoPlan = PrimasPersonaCotizacion.CodigoPlan ";
            strSql += "INNER JOIN Polizas ON Polizas.CodigoPoliza = Planes.CodigoPoliza ";
            strSql += "INNER JOIN PersonaCotizacion ON PersonaCotizacion.CodigoPersona = PrimasPersonaCotizacion.CodigoPersona ";
            strSql += "WHERE PrimasPersonaCotizacion.CodigoCotizacion = @CodigoCotizacion AND CodigoFormaPago = @CodigoFormaPago AND CodigoProducto = @CodigoProducto ";
            strSql += "ORDER BY CodigoCobertura,CodigoTipoPersonaCotizacion,CodigoPlan ";

            oLocalConnection = oConexion.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(strSql, oLocalConnection);
            da.SelectCommand.Parameters.Add("@CodigoCotizacion", SqlDbType.Int).Value = nCodigoCotizacion;
            da.SelectCommand.Parameters.Add("@CodigoFormaPago", SqlDbType.Int).Value = nCodigoFormaPago;
            da.SelectCommand.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = nCodigoProducto;
            da.SelectCommand.CommandType = System.Data.CommandType.Text;
            da.Fill(dt);

            oLocalConnection.Close();
            oLocalConnection.Dispose();
            da.Dispose();

            return dt;
        }
    }

    public class CorreoLoyal
    {
        public int nCodigoUsuario;
        ConsultasTemplate oConsultasTemplates = new ConsultasTemplate();
        ProcesoCorreo oProcesoCorreo = new ProcesoCorreo();
        Correo oCorreo = new Correo();
        //clsUsuario oUsuario = new clsUsuario();


        string strRutaTemplate = @"C:\inetpub\wwwroot\assets\correotemplates\";
        string strRutaPDF = @"PDF\";
        string strRutaTemplatePDF = @"C:\inetpub\wwwroot\assets\templates\";

        clsBaseDatos.Conexion oConexion = new clsBaseDatos.Conexion();

        private string AttachmentDocumento(int nCodigoDocumento)
        {
            string sfileName = "";
            if (nCodigoDocumento > 0)
            {
                DataTable dt = new DataTable();
                SqlConnection oLocalConnection = new SqlConnection();

                oLocalConnection = oConexion.ObtenerConexion();
                SqlDataAdapter da = new SqlDataAdapter("SELECT CodigoDocumento,Documento,NombreDocumento FROM Documentos WHERE CodigoDocumento = @CodigoDocumento", oLocalConnection);
                da.SelectCommand.CommandType = System.Data.CommandType.Text;
                da.SelectCommand.Parameters.Add("@CodigoDocumento", SqlDbType.Int).Value = nCodigoDocumento;
                da.Fill(dt);

                oLocalConnection.Close();
                oLocalConnection.Dispose();
                da.Dispose();

                byte[] oDocumentoByte = (byte[])dt.Rows[0]["Documento"];

                Directory.CreateDirectory(@"C:\TempLoyal");
                DirectorySecurity sec = Directory.GetAccessControl(@"C:\TempLoyal");

                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.SetAccessControl(@"C:\TempLoyal", sec);


                sfileName = @"C:\TempLoyal\" + dt.Rows[0]["NombreDocumento"].ToString();
                File.WriteAllBytes(sfileName, oDocumentoByte);
            }
            else
            {
                sfileName = "";
            }


            return sfileName;
        }

        public bool CorreoEnvioCotizacion(int nCodigoCotizacion, int nCodigoProducto, int nCodigoUsuario, string sCorreo, string sCorreoCopia, string sIdioma)
        {
            DataTable dtCorreo = oConsultasTemplates.CorreoCotizacion(nCodigoCotizacion, nCodigoProducto);

            string strProducto = "";

            if (nCodigoProducto == 1)
            { strProducto = "BEYOND"; }

            if (nCodigoProducto == 2)
            { strProducto = "PRIVILEGE"; }

            if (nCodigoProducto == 3)
            { strProducto = "LIBERTY"; }

            if (nCodigoProducto == 4)
            { strProducto = "LEGACY"; }

            string sBody = "";

            StreamReader srMail = new StreamReader(strRutaTemplate + "LOYALCorreoEnvioCotizacion" + strProducto.Trim() + sIdioma + ".html");
            sBody = srMail.ReadToEnd();

            //sBody = sBody.Replace("{SaludoCliente}", "Estimado <strong>" + dtCorreo.Rows[0]["NombreCompleto"].ToString() + "</strong>");


            oCorreo.RutaArchivo = new string[2];
            oCorreo.RutaArchivo[0] = strRutaTemplatePDF + strProducto.Trim() + "-" + sIdioma + "-2021.pdf";

            int nCodigoDocumentoCotizacion;

            if(dtCorreo.Rows[0]["CodigoDocumento"] == DBNull.Value)
            { 
                Cotizaciones oCotizacion = new Cotizaciones();
                nCodigoDocumentoCotizacion = oCotizacion.ObtenerCodigoPDFCotizacion(nCodigoCotizacion, nCodigoProducto, nCodigoUsuario, sIdioma);
            }
            else
            { 
                nCodigoDocumentoCotizacion = Convert.ToInt32(dtCorreo.Rows[0]["CodigoDocumento"]); 
            }

            oCorreo.RutaArchivo[1] = AttachmentDocumento(nCodigoDocumentoCotizacion);

            //oUsuario.CargaCorreo(nCodigoUsuario);
            oCorreo.NombreDe = "Loyalig quote";
            oCorreo.De = "quote@loyalig.com";
            oCorreo.Para = sCorreo;
            if (sCorreoCopia != "")
            { oCorreo.Copia = sCorreoCopia; }
            if (sIdioma == "en")
            { oCorreo.Asunto = "LOYAL Quote No. " + nCodigoCotizacion.ToString() + " LOYAL " + strProducto; }
            else
            { oCorreo.Asunto = "LOYAL Cotización Nro. " + nCodigoCotizacion.ToString() + " LOYAL " + strProducto; }

            oCorreo.Texto = sBody;
            oCorreo.Password = "LoyalIG1234!";

            oProcesoCorreo.EnviarCorreo(oCorreo);



            return true;
        }

    }
}
