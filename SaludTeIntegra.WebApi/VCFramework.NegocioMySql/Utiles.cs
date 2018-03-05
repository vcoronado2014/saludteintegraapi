using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Web;

namespace VCFramework.NegocioMySQL
{
    public class Utiles
    {
        public const string HTML_DOCTYPE = "text/html";
        public const string JSON_DOCTYPE = "application/json";
        public const string XML_DOCTYPE = "application/xml";


        //metodos para enviar correo nuevos
        public static bool EnviarCorreoCreacionUsuario(string email, string nombreUsuario, string password)
        {
            bool retorno = false;
            DateTime fechaActual = DateTime.Now;
            string Fecha = fechaActual.ToString("dd/MM/yyyy");
            string hora = fechaActual.ToString("HH:mm");
            string UrlLogo = ConfigurationManager.AppSettings["Logo"].ToString();
            string correoAgenda = ConfigurationManager.AppSettings["correoEmisor"].ToString();
            string nombreCorreoAgenda = ConfigurationManager.AppSettings["nombreCorreoEmisor"].ToString();
            string usuarioSmtp = ConfigurationManager.AppSettings["usuarioSmtp"].ToString();
            string contrasenaSmtp = ConfigurationManager.AppSettings["contrasenaSmtp"].ToString();
            string smtpUrl = ConfigurationManager.AppSettings["smtpUrl"].ToString();
            int portSmtp = Int32.Parse(ConfigurationManager.AppSettings["portSmtp"].ToString());
            string nombreArchivo = ConfigurationManager.AppSettings["nombreArchivo"].ToString();

            var client = new SmtpClient(smtpUrl, portSmtp)
            {
                Credentials = new NetworkCredential(usuarioSmtp, contrasenaSmtp),
                EnableSsl = true
            };
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(correoAgenda, nombreCorreoAgenda);
            msg.To.Add(new MailAddress(email));
            msg.Subject = "Alta de Usuario";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\" color=\"#666666\"><tbody><tr><td width=\"100%\" valign=\"top\" bgcolor=\"#ffffff\"><table width=\"570\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\"><tbody><tr bgcolor=\"#ffffff\"><td valign=\"top\" style=\"padding:10px 0 10px 0;color:#ffffff;background:#4f5f6f;\"><center><img style=\"vertical-align:top;max-width:220px\" src=\"cid:LOGO\" alt=\"Salud Te Integra\" width=\"110\" class=\"CToWUd\"></center></td></tr><tr><td style=\"padding:20px 50px 0px 50px\"><p style=\"font-size:24px\"><strong style=\"margin-bottom:4px;display:inline-block\">Comprobante de reserva de cita</strong></p><p>Su cuenta ha sido creada con éxito:</p><p><strong>Nombre de Usuario:</strong>  " + nombreUsuario + ".<br><strong>Password:</strong> " + password + " <br></p><small style=\"font-size:10px\">Salud Te Integra es un servicio creado por Rayen Salud SPA.</small></td></tr></tbody></table></td></tr></tbody></table>";
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            //System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
            //contype.Parameters.Add("method", "REQUEST");
            //contype.Parameters.Add("name", nombreArchivo);
            //contype.Parameters.Add("CharSet", "UTF-8");

            string attachmentPath = HttpRuntime.AppDomainAppPath + @UrlLogo;
            Attachment inline = new Attachment(attachmentPath);
            inline.ContentDisposition.Inline = true;
            inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            inline.ContentId = "LOGO";
            inline.ContentType.MediaType = "image/png";
            inline.ContentType.Name = Path.GetFileName(attachmentPath);
            msg.Attachments.Add(inline);

            //string content = str.ToString();
            //UTF8Encoding encoder = new UTF8Encoding();
            //byte[] bytes = Encoding.UTF8.GetBytes(content);
            //string contentEncoding = encoder.GetString(bytes);

            //msg.Attachments.Add(new Attachment(new System.IO.MemoryStream(bytes), contype));

            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                throw ex;
            }

        }

        public static bool EnviarCorreoRecuperacionClave(string email, string nombreUsuario, string password)
        {
            bool retorno = false;
            DateTime fechaActual = DateTime.Now;
            string Fecha = fechaActual.ToString("dd/MM/yyyy");
            string hora = fechaActual.ToString("HH:mm");
            string UrlLogo = ConfigurationManager.AppSettings["Logo"].ToString();
            string correoAgenda = ConfigurationManager.AppSettings["correoEmisor"].ToString();
            string nombreCorreoAgenda = ConfigurationManager.AppSettings["nombreCorreoEmisor"].ToString();
            string usuarioSmtp = ConfigurationManager.AppSettings["usuarioSmtp"].ToString();
            string contrasenaSmtp = ConfigurationManager.AppSettings["contrasenaSmtp"].ToString();
            string smtpUrl = ConfigurationManager.AppSettings["smtpUrl"].ToString();
            int portSmtp = Int32.Parse(ConfigurationManager.AppSettings["portSmtp"].ToString());
            string nombreArchivo = ConfigurationManager.AppSettings["nombreArchivo"].ToString();

            var client = new SmtpClient(smtpUrl, portSmtp)
            {
                Credentials = new NetworkCredential(usuarioSmtp, contrasenaSmtp),
                EnableSsl = true
            };
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(correoAgenda, nombreCorreoAgenda);
            msg.To.Add(new MailAddress(email));
            msg.Subject = "Recuperación de Clave";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\" color=\"#666666\"><tbody><tr><td width=\"100%\" valign=\"top\" bgcolor=\"#ffffff\"><table width=\"570\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\"><tbody><tr bgcolor=\"#ffffff\"><td valign=\"top\" style=\"padding:10px 0 10px 0;color:#ffffff;background:#4f5f6f;\"><center><img style=\"vertical-align:top;max-width:220px\" src=\"cid:LOGO\" alt=\"Salud Te Integra\" width=\"110\" class=\"CToWUd\"></center></td></tr><tr><td style=\"padding:20px 50px 0px 50px\"><p style=\"font-size:24px\"><strong style=\"margin-bottom:4px;display:inline-block\">Recuperación de Clave</strong></p><p>Estimado Usuario su clave ha sido recuperada con éxito:</p><p><strong>Nombre de Usuario:</strong>  " + nombreUsuario + ".<br><strong>Su Clave:</strong> " + password + " <br></p><small style=\"font-size:10px\">Salud Te Integra es un servicio creado por Rayen Salud SPA.</small></td></tr></tbody></table></td></tr></tbody></table>";
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            //System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
            //contype.Parameters.Add("method", "REQUEST");
            //contype.Parameters.Add("name", nombreArchivo);
            //contype.Parameters.Add("CharSet", "UTF-8");

            string attachmentPath = HttpRuntime.AppDomainAppPath + @UrlLogo;
            Attachment inline = new Attachment(attachmentPath);
            inline.ContentDisposition.Inline = true;
            inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            inline.ContentId = "LOGO";
            inline.ContentType.MediaType = "image/png";
            inline.ContentType.Name = Path.GetFileName(attachmentPath);
            msg.Attachments.Add(inline);

            //string content = str.ToString();
            //UTF8Encoding encoder = new UTF8Encoding();
            //byte[] bytes = Encoding.UTF8.GetBytes(content);
            //string contentEncoding = encoder.GetString(bytes);

            //msg.Attachments.Add(new Attachment(new System.IO.MemoryStream(bytes), contype));

            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                throw ex;
            }

        }

        public static bool EnviarCorreoCambioClave(string email, string nombreUsuario, string password)
        {
            bool retorno = false;
            DateTime fechaActual = DateTime.Now;
            string Fecha = fechaActual.ToString("dd/MM/yyyy");
            string hora = fechaActual.ToString("HH:mm");
            string UrlLogo = ConfigurationManager.AppSettings["Logo"].ToString();
            string correoAgenda = ConfigurationManager.AppSettings["correoEmisor"].ToString();
            string nombreCorreoAgenda = ConfigurationManager.AppSettings["nombreCorreoEmisor"].ToString();
            string usuarioSmtp = ConfigurationManager.AppSettings["usuarioSmtp"].ToString();
            string contrasenaSmtp = ConfigurationManager.AppSettings["contrasenaSmtp"].ToString();
            string smtpUrl = ConfigurationManager.AppSettings["smtpUrl"].ToString();
            int portSmtp = Int32.Parse(ConfigurationManager.AppSettings["portSmtp"].ToString());
            string nombreArchivo = ConfigurationManager.AppSettings["nombreArchivo"].ToString();

            var client = new SmtpClient(smtpUrl, portSmtp)
            {
                Credentials = new NetworkCredential(usuarioSmtp, contrasenaSmtp),
                EnableSsl = true
            };
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(correoAgenda, nombreCorreoAgenda);
            msg.To.Add(new MailAddress(email));
            msg.Subject = "Cambio de Clave";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\" color=\"#666666\"><tbody><tr><td width=\"100%\" valign=\"top\" bgcolor=\"#ffffff\"><table width=\"570\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" bgcolor=\"#ffffff\"><tbody><tr bgcolor=\"#ffffff\"><td valign=\"top\" style=\"padding:10px 0 10px 0;color:#ffffff;background:#4f5f6f;\"><center><img style=\"vertical-align:top;max-width:220px\" src=\"cid:LOGO\" alt=\"Salud Te Integra\" width=\"110\" class=\"CToWUd\"></center></td></tr><tr><td style=\"padding:20px 50px 0px 50px\"><p style=\"font-size:24px\"><strong style=\"margin-bottom:4px;display:inline-block\">Recuperación de Clave</strong></p><p>Estimado Usuario su clave ha sido cambiada con éxito:</p><p><strong>Nombre de Usuario:</strong>  " + nombreUsuario + ".<br><strong>Su nueva Clave:</strong> " + password + " <br></p><small style=\"font-size:10px\">Salud Te Integra es un servicio creado por Rayen Salud SPA.</small></td></tr></tbody></table></td></tr></tbody></table>";
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            //System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
            //contype.Parameters.Add("method", "REQUEST");
            //contype.Parameters.Add("name", nombreArchivo);
            //contype.Parameters.Add("CharSet", "UTF-8");

            string attachmentPath = HttpRuntime.AppDomainAppPath + @UrlLogo;
            Attachment inline = new Attachment(attachmentPath);
            inline.ContentDisposition.Inline = true;
            inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            inline.ContentId = "LOGO";
            inline.ContentType.MediaType = "image/png";
            inline.ContentType.Name = Path.GetFileName(attachmentPath);
            msg.Attachments.Add(inline);

            //string content = str.ToString();
            //UTF8Encoding encoder = new UTF8Encoding();
            //byte[] bytes = Encoding.UTF8.GetBytes(content);
            //string contentEncoding = encoder.GetString(bytes);

            //msg.Attachments.Add(new Attachment(new System.IO.MemoryStream(bytes), contype));

            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                throw ex;
            }

        }

        public static string NLogs(string mensaje)
        {
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            logger.Log(NLog.LogLevel.Info, mensaje);
            return mensaje;
        }
        public static string NLogs(Exception ex)
        {
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            logger.LogException(NLog.LogLevel.Error, "Error", ex);
            return ex.Message;
        }

        public static Image NonLockingOpen(string filename)
        {
            Image result;

            #region Save file to byte array

            long size = (new FileInfo(filename)).Length;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[size];
            try
            {
                fs.Read(data, 0, (int)size);
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            #endregion

            #region Convert bytes to image

            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, (int)size);
            result = new Bitmap(ms);
            ms.Close();

            #endregion

            return result;
        }

        public static String DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            Int32 anios;
            Int32 meses;
            Int32 dias;
            Int32 horas;
            Int32 minutos;
            Int32 segundos;
            String str = "";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);
            horas = (newdt.Hour - olddt.Hour);
            minutos = (newdt.Minute - olddt.Minute);
            segundos = (newdt.Second - olddt.Second);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "Fecha Invalida";
            }
            if (anios > 0)
                str = str + anios.ToString() + " años ";
            if (meses > 0)
                str = str + meses.ToString() + " meses ";
            if (dias > 0)
                str = str + dias.ToString() + " dias ";
            if (horas > 0)
                str = str + horas.ToString() + " horas ";
            if (minutos > 0)
                str = str + minutos.ToString() + " minutos ";
            if (anios == 0 && meses == 0 && dias == 0 && horas == 0 && minutos == 0)
                str = segundos.ToString() + " segundos ";


            return "hace " + str;
        }
        public static int EntregaEntero(string valorDosDigitos)
        {
            int retorno = 0;

            if (valorDosDigitos.Length == 2)
            {
                string valorUno = valorDosDigitos.Substring(0, 1);
                string valorDos = valorDosDigitos.Substring(1, 1);
                if (valorUno == "0")
                {
                    retorno = int.Parse(valorDos);
                }
                else
                {
                    retorno = int.Parse(valorDosDigitos);
                }
            }

            return retorno;
        }
        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public static bool ValidaEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        //public static byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        //public static string Encripta(string Cadena, string Password)
        //{
        //    byte[] Clave = Encoding.ASCII.GetBytes(Password);
        //    byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
        //    byte[] encripted;
        //    RijndaelManaged cripto = new RijndaelManaged();
        //    using (MemoryStream ms = new MemoryStream(inputBytes.Length))
        //    {
        //        using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
        //        {
        //            objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
        //            objCryptoStream.FlushFinalBlock();
        //            objCryptoStream.Close();
        //        }
        //        encripted = ms.ToArray();
        //    }
        //    return Convert.ToBase64String(encripted);
        //}



        //public static string Desencripta(string Cadena, string Password)
        //{
        //    byte[] Clave = Encoding.ASCII.GetBytes(Password);
        //    byte[] inputBytes = Convert.FromBase64String(Cadena);
        //    byte[] resultBytes = new byte[inputBytes.Length];
        //    string textoLimpio = String.Empty;
        //    RijndaelManaged cripto = new RijndaelManaged();
        //    using (MemoryStream ms = new MemoryStream(inputBytes))
        //    {
        //        using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
        //        {
        //            using (StreamReader sr = new StreamReader(objCryptoStream, true))
        //            {
        //                textoLimpio = sr.ReadToEnd();
        //            }
        //        }
        //    }
        //    return textoLimpio;
        //}

        // Connection String
        //public const string ConnStr =
        //   "Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database=bdcolegios_mysql;uid=root;pwd=co2008;option=3";
        //public const string ConnStr =
        //   "Driver={MySQL ODBC 5.1 Driver};Server=MYSQL5011.Smarterasp.net;Database=db_9dac90_cole;User=9dac90_cole;Password=antonia2006;Option=3;";

        public static string ConnStr()
        {
            string cns = "Driver={MySQL ODBC 5.1 Driver};Server=MYSQL5011.Smarterasp.net;Database=db_9dac90_cole;User=9dac90_cole;Password=antonia2006;Option=3;";
            if (System.Configuration.ConfigurationManager.ConnectionStrings["BDColegioSql"].ConnectionString != null)
                cns = System.Configuration.ConfigurationManager.ConnectionStrings["BDColegioSql"].ConnectionString;
            return cns;
        }

        public const string CNS = "BDColegioSql";
        
        public static string NombreBaseDatos()
        {
            string retorno = "'db_9dac90_cole'";

            if (System.Configuration.ConfigurationManager.AppSettings["NOMBRE_BD"] != null)
            {
                retorno = "'" + System.Configuration.ConfigurationManager.AppSettings["NOMBRE_BD"].ToString() + "'";
            }

            return retorno;
        }
        public static string SMTP()
        {
            string retorno = "smtp.gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["SMTP"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString();
            }

            return retorno;
        }
        public static string PUERTO_SMTP()
        {
            string retorno = "587";
            if (System.Configuration.ConfigurationManager.AppSettings["PUERTO_SMTP"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["PUERTO_SMTP"].ToString();
            }

            return retorno;
        }
        public static string NOMBRE_SERVIDOR_CORREO()
        {
            string retorno = "vcoronado.alarcon@gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["NOMBRE_SERVIDOR_CORREO"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["NOMBRE_SERVIDOR_CORREO"].ToString();
            }

            return retorno;
        }
        public static string CLAVE_SERVIDOR_CORREO()
        {
            string retorno = "antonia2005";
            if (System.Configuration.ConfigurationManager.AppSettings["CLAVE_SERVIDOR_CORREO"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["CLAVE_SERVIDOR_CORREO"].ToString();
            }

            return retorno;
        }
        public static string ENABLE_SSL()
        {
            string retorno = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["ENABLE_SSL"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["ENABLE_SSL"].ToString();
            }

            return retorno;
        }
        public static string RetornaFechaFormateadaServidor(string fechaServidor)
        {
            string retorno = "";
            //lo primero es descomponer la fecha
            string[] fechas = fechaServidor.Split('/');
            if (fechas != null && fechas.Length == 3)
            {
                string dia = "";
                if (fechas[1].Length == 1)
                {
                    dia = "0" + fechas[1].ToString();
                }
                else
                {
                    dia = fechas[1].ToString();
                }
                string mes = "";
                if (fechas[0].Length == 1)
                {
                    mes = "0" + fechas[0].ToString();
                }
                else
                {
                    dia = fechas[0].ToString();
                }
                string anno = fechas[2].ToString();

                retorno = dia + "-" + mes + "-" + anno;
            }
            else
                retorno = DateTime.Now.ToShortDateString();


            return retorno;
        }

        public static string RetornaFechaEntera()
        {
            DateTime fechaServidor = DateTime.Now;
            string retorno = "";
            string anno = fechaServidor.Year.ToString();
            string mes = "";
            string dia = "";
            if (fechaServidor.Month < 10)
                mes = "0" + fechaServidor.Month.ToString();
            else
                mes = fechaServidor.Month.ToString();

            if (fechaServidor.Day < 10)
                dia = "0" + fechaServidor.Day.ToString();
            else
                dia = fechaServidor.Day.ToString();

            retorno = anno + mes + dia;

            return retorno;
        }
        public static string RetornaHoraEntera()
        {
            DateTime fechaServidor = DateTime.Now;
            string retorno = "";
            string hora = "";
            string minutos = "";
            if (fechaServidor.Hour < 10)
                hora = "0" + fechaServidor.Hour.ToString();
            else
                hora = fechaServidor.Hour.ToString();

            if (fechaServidor.Minute < 10)
                minutos = "0" + fechaServidor.Minute.ToString();
            else
                minutos = fechaServidor.Minute.ToString();

            retorno = hora + minutos;

            return retorno;
        }
        public static int RetornaFechaEntera(DateTime fechaProcesar)
        {
            DateTime fechaServidor = fechaProcesar;
            int retorno = 0;
            string anno = fechaServidor.Year.ToString();
            string mes = "";
            string dia = "";
            if (fechaServidor.Month < 10)
                mes = "0" + fechaServidor.Month.ToString();
            else
                mes = fechaServidor.Month.ToString();

            if (fechaServidor.Day < 10)
                dia = "0" + fechaServidor.Day.ToString();
            else
                dia = fechaServidor.Day.ToString();

            retorno = int.Parse(anno + mes + dia);

            return retorno;
        }
        public static string RetornaFechaDocumento(string fechaProcesar)
        {
            //12/03/2017 10:07 p.m.
            //5/17/2017 7:06 PM

            string retorno = RetornaFechaFormateadaServidor("09/09/2017");
            try
            {
                //primera separacion
                string[] parte1 = fechaProcesar.Split(' ');
                if (parte1.Length > 1)
                {
                    //segunda separación
                    string[] parte2 = parte1[0].Split('/');
                    if (parte2.Length == 3)
                    {
                        if (parte2[1].Length == 1)
                            parte2[1] = "0" + parte2[1];
                        if (parte2[0].Length == 1)
                            parte2[0] = "0" + parte2[0];

                        retorno = parte2[1] + "-" + parte2[0] + "-" + parte2[2];

                    }
                }

            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.Log(ex);
            }


            return retorno;
        }

        /// <summary>
        /// Entrega Fecha entera a partir del formato 21-01-2017
        /// </summary>
        /// <param name="fechaProcesar">Fecha string</param>
        /// <returns></returns>
        public static int RetornaFechaEnteraStr(string fechaProcesar)
        {
            string[] fechitas = fechaProcesar.Split('-');
            int retorno = 0;
            try
            {
                if (fechitas != null && fechitas.Length > 2)
                {
                    string anio = fechitas[2].Split(' ')[0];
                    retorno = int.Parse(anio + fechitas[1] + fechitas[0]);
                }
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.Log(ex);
            }
            return retorno;
        }
        public static string ConstruyeFechaDos(DateTime fecha)
        {
            string retorno = "";
            string dia = "";
            string mes = "";
            string anno = "";
            string hora = "";
            string minutos = "";
            string segundos = "";


            if (fecha.Day < 10)
                dia = "0" + fecha.Day.ToString();
            else
                dia = fecha.Day.ToString();

            if (fecha.Month < 10)
                mes = "0" + fecha.Month.ToString();
            else
                mes = fecha.Month.ToString();

            if (fecha.Hour < 10)
                hora = "0" + fecha.Hour.ToString();
            else
                hora = fecha.Hour.ToString();

            if (fecha.Minute < 10)
                minutos = "0" + fecha.Minute.ToString();
            else
                minutos = fecha.Minute.ToString();

            if (fecha.Second < 10)
                segundos = "0" + fecha.Second.ToString();
            else
                segundos = fecha.Second.ToString();

            anno = fecha.Year.ToString();

            retorno = dia + "-" + mes + "-" + anno + " " + hora + ":" + minutos;
            return retorno;
        }
        public static string ConstruyeFecha(DateTime fecha)
        {
            string retorno = "";
            string dia = "";
            string mes = "";
            string anno = "";
            string hora = "";
            string minutos = "";
            string segundos = "";


            if (fecha.Day < 10)
                dia = "0" + fecha.Day.ToString();
            else
                dia = fecha.Day.ToString();

            if (fecha.Month < 10)
                mes = "0" + fecha.Month.ToString();
            else
                mes = fecha.Month.ToString();

            if (fecha.Hour < 10)
                hora = "0" + fecha.Hour.ToString();
            else
                hora = fecha.Hour.ToString();

            if (fecha.Minute < 10)
                minutos = "0" + fecha.Minute.ToString();
            else
                minutos = fecha.Minute.ToString();

            if (fecha.Second < 10)
                segundos = "0" + fecha.Second.ToString();
            else
                segundos = fecha.Second.ToString();

            anno = fecha.Year.ToString();

            retorno = anno + "-" + mes + "-" + dia + " " + hora + ":" + segundos;
            return retorno;
        }
        public static void Log(string mensaje)
        {
            string carpetaArchivo = @"Logs\log.txt";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;

            object Locker = new object();
            XmlDocument _doc = new XmlDocument();

            try
            {
                if (!File.Exists(rutaFinal))
                {
                    File.Create(rutaFinal);
                }

                _doc.Load(rutaFinal);

                lock (Locker)
                {
                    //var id = (XmlElement)_doc.DocumentElement.LastChild;
                    //id.GetElementsByTagName("Id");
                    int cantidad = _doc.ChildNodes.Count;
                    int indice = 1;
                    if (cantidad > 0)
                    {
                        //obtener el ultimo elemento id
                        if ((XmlElement)_doc.DocumentElement.LastChild != null)
                        {
                            var ultimo = (XmlElement)_doc.DocumentElement.LastChild;
                            indice = int.Parse(ultimo.LastChild.InnerText);
                            indice = indice + 1;
                        }
                    }

                    var el = (XmlElement)_doc.DocumentElement.AppendChild(_doc.CreateElement("error"));
                    //el.SetAttribute("Fecha", ConstruyeFecha(DateTime.Now));

                    el.AppendChild(_doc.CreateElement("Fecha")).InnerText = ConstruyeFecha(DateTime.Now);
                    el.AppendChild(_doc.CreateElement("Detalle")).InnerText = mensaje;
                    el.AppendChild(_doc.CreateElement("Id")).InnerText = indice.ToString();
                    _doc.Save(rutaFinal);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public static string EntregaNombreArchivo(string nombreArchivo)
        {
            StringBuilder sb = new StringBuilder();
            string[] texto = nombreArchivo.ToString().Split(' ');
            string nuevoNombre = string.Empty;
            if (texto.Length > 0)
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(texto[i]);
                        sb.Append(" ");
                    }
                }
            }
            return sb.ToString();
        }
        public static string ObtenerMensajeXML(string nombre, bool esNuevo, bool esModificado, bool esEliminado)
        {
            string retorno = "";
            string carpetaArchivo = @"Mensajes.xml";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaFinal);

            try
            {
                XmlNodeList mensaje = doc.GetElementsByTagName("Mensaje");
                XmlNodeList lista = ((XmlElement)mensaje[0]).GetElementsByTagName("item");
                if (lista != null && lista.Count > 0)
                {
                    foreach (XmlElement nodo in lista)
                    {
                        if (nodo != null)
                        {
                            if (nodo.Attributes[0] != null)
                            {
                                if (nodo.Attributes[0].InnerText.ToString().ToUpper() == nombre.ToUpper())
                                {
                                    string otraBusqueda = "nuevo";
                                    if (esNuevo)
                                        otraBusqueda = "nuevo";
                                    if (esModificado)
                                        otraBusqueda = "modificado";
                                    if (esEliminado)
                                        otraBusqueda = "eliminado";

                                    if (nodo.ChildNodes != null && nodo.ChildNodes.Count > 0)
                                    {
                                        foreach (XmlElement nodito in nodo.ChildNodes)
                                        {
                                            if (nodito.Name.ToUpper() == otraBusqueda.ToUpper())
                                            {
                                                retorno = nodito.InnerXml;
                                                break;
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.Log(ex);
            }

            return retorno;
        }
        public static string ObtenerMensajeXML(string nombre, bool esNuevo)
        {
            string retorno = "";
            string carpetaArchivo = @"Mensajes.xml";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaFinal);

            try
            {
                XmlNodeList mensaje = doc.GetElementsByTagName("Mensaje");
                XmlNodeList lista = ((XmlElement)mensaje[0]).GetElementsByTagName("item");
                if (lista != null && lista.Count > 0)
                {
                    foreach (XmlElement nodo in lista)
                    {
                        if (nodo != null)
                        {
                            if (nodo.Attributes[0] != null)
                            {
                                if (nodo.Attributes[0].InnerText.ToString().ToUpper() == nombre.ToUpper())
                                {
                                    string otraBusqueda = "nuevo";
                                    if (!esNuevo)
                                        otraBusqueda = "modificado";

                                    if (nodo.ChildNodes != null && nodo.ChildNodes.Count > 0)
                                    {
                                        foreach (XmlElement nodito in nodo.ChildNodes)
                                        {
                                            if (nodito.Name.ToUpper() == otraBusqueda.ToUpper())
                                            {
                                                retorno = nodito.InnerXml;
                                                break;
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.Log(ex);
            }

            return retorno;
        }

        public static string ObtenerUrl()
        {
            string retorno = "http://www.cpas.cl";
            try
            {
                retorno = System.Web.HttpContext.Current.Request.Url.Host;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return retorno;
        }
        public static void Log(Exception mensaje)
        {
            string carpetaArchivo = @"Logs\log.txt";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;

            object Locker = new object();
            XmlDocument _doc = new XmlDocument();

            try
            {
                if (!File.Exists(rutaFinal))
                {
                    File.Create(rutaFinal);
                }

                _doc.Load(rutaFinal);

                lock (Locker)
                {
                    //var id = (XmlElement)_doc.DocumentElement.LastChild;
                    //id.GetElementsByTagName("Id");
                    int cantidad = _doc.ChildNodes.Count;
                    int indice = 1;
                    if (cantidad > 0)
                    {
                        //obtener el ultimo elemento id
                        if ((XmlElement)_doc.DocumentElement.LastChild != null)
                        {
                            var ultimo = (XmlElement)_doc.DocumentElement.LastChild;
                            indice = int.Parse(ultimo.LastChild.InnerText);
                            indice = indice + 1;
                        }
                    }

                    var el = (XmlElement)_doc.DocumentElement.AppendChild(_doc.CreateElement("error"));
                    //el.SetAttribute("Fecha", ConstruyeFecha(DateTime.Now));

                    el.AppendChild(_doc.CreateElement("Fecha")).InnerText = ConstruyeFecha(DateTime.Now);
                    el.AppendChild(_doc.CreateElement("Detalle")).InnerText = mensaje.Message;
                    el.AppendChild(_doc.CreateElement("Id")).InnerText = indice.ToString();
                    _doc.Save(rutaFinal);
                }

            }
            catch (Exception ex)
            {

            }


        }
        /// <summary>
        /// Validar Rut en el formato 12.333.66-K
        /// </summary>
        /// <param name="rut">Rut Formateado</param>
        /// <returns></returns>
        public static bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}
