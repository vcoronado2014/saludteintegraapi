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
    public class ClaveController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        {
        }
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.Email == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Email, recuperar clave");
            }
            else if (data.NombreUsuario == "") {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Nombre usuario, recuperar clave");
            }
            else if (data.Password == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Password, recuperar clave");
            }
            else
            {
                try
                {
                    string email = data.Email;
                    string nomberUsuario = data.NombreUsuario;
                    string password = data.Password;
                    bool resultado = VCFramework.NegocioMySQL.Utiles.EnviarCorreoRecuperacionClave(email, nomberUsuario, password);

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, resultado);


                }
                catch (Exception ex)
                {
                    VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                    httpResponse = ManejoMensajes.RetornaMensajeExcepcion(httpResponse, ex);
                }
            }
            return httpResponse;


        }

        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Get([FromUri]string email, [FromUri]string nombreUsuario, [FromUri]string password)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            //validaciones antes de ejecutar la llamada.
            //este id corresponde al AusId
            if (nombreUsuario == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "nombre de usuario");
            }
            else if (email == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "email");
            }
            else if (password == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "password");
            }
            else
            {
                try
                {
                    bool resultado = VCFramework.NegocioMySQL.Utiles.EnviarCorreoCambioClave(email, nombreUsuario, password);

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, resultado);
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