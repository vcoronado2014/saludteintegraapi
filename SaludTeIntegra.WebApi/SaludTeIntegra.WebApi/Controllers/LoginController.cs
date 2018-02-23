using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Xml;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using VCFramework.Entidad;
using VCFramework.NegocioMySQL;

namespace SaludTeIntegra.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController: ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.usuario == "")
                throw new ArgumentNullException("Usuario");
            if (data.password == "")
                throw new ArgumentNullException("Password");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string usu = data.usuario;
                string pass = data.password;
                
                List<VCFramework.Entidad.AutentificacionUsuario> lista = VCFramework.NegocioMySql.AutentificacionUsuario.Listar();
                VCFramework.Entidad.Resultado result = new Resultado();
                result.Datos = lista;
                result.Mensaje = new Mensaje();
                result.Mensaje.Codigo = 0;
                result.Mensaje.Texto = "Correcto";
                //VCFramework.NegocioMySQL.Utiles.NLogs("prueba de mensaje");
                if (lista != null)
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(result);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
             
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                //VCFramework.Entidad.Resultado result = new Resultado();
                //result.Datos = null;
                //result.Mensaje = new Mensaje();
                //result.Mensaje.Codigo = 1000;
                //result.Mensaje.Texto = ex.Message;
                //httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                //String JSON = JsonConvert.SerializeObject(result);
                //httpResponse.Content = new StringContent(JSON);
                //httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

                //throw ex;
                httpResponse = ManejoMensajes.RetornaMensajeExcepcion(httpResponse, ex);
            }
            return httpResponse;


        }


    }
}