using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Xml;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SaludTeIntegra.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImpresionController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        {
        }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.AusId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "AusId");
            }
            //password no es requerido ya que puede ser una actualizaciòn de usuario
            else if (data.Run == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Run");
            }
            else if (data.EcolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Ecol Id");
            }
            else if (data.FechaAtencion == null)
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Fecha Atencion");
            }
            else
            {
                try
                {
                    //variables
                    int nuevoId = 0;
                    string ausId = data.AusId;
                    string run = data.Run;
                    string ecolId = data.EcolId;
                    DateTime fechaAtencion =  data.FechaAtencion;
                    string fechaActual = VCFramework.NegocioMySQL.Utiles.ConstruyeFechaDos(DateTime.Now);
                    string fechaAt = fechaAtencion.ToShortDateString() + " " + fechaAtencion.ToShortTimeString();

                    VCFramework.Entidad.RegistroImpresion entidad  = new VCFramework.Entidad.RegistroImpresion();
                    entidad.AusId = int.Parse(ausId);
                    entidad.EcolId = int.Parse(ecolId);
                    entidad.Fecha = fechaActual;

                    entidad.FechaAtencion = fechaAt.Replace("/","-");
                    entidad.Run = run.Replace(".", "").Replace("-", "");

                    nuevoId = VCFramework.NegocioMySql.RegistroImpresion.Insertar(entidad);
                    entidad.Id = nuevoId;

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, entidad, EnumMensajes.Registro_modificado_con_exito);


                }
                catch (Exception ex)
                {
                    VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                    httpResponse = ManejoMensajes.RetornaMensajeExcepcion(httpResponse, ex);
                }
            }

            return httpResponse;
        }
    }
}