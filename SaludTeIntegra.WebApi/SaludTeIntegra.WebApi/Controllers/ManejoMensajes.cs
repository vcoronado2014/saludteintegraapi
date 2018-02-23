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
    public class ManejoMensajes
    {
        public static HttpResponseMessage RetornaMensajeExcepcion(HttpResponseMessage httpResponse, Exception ex)
        {
            VCFramework.Entidad.Resultado result = new Resultado();
            result.Datos = null;
            result.Mensaje = new Mensaje();
            result.Mensaje.Codigo = 1000;
            result.Mensaje.Texto = ex.Message;
            httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            String JSON = JsonConvert.SerializeObject(result);
            httpResponse.Content = new StringContent(JSON);
            httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
            return httpResponse;
        }
    }
}