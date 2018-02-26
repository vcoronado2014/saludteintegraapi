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
    public class VisorController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        {
        }
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.*
            if (data.UspId == "")
                throw new ArgumentNullException("UspId");
            string run = "";
            string idRyf = "";
            if (data.Run != "")
                run = data.Run;
            if (data.IdRyf != "")
                idRyf = data.IdRyf;


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {

                ServicioVisor.visor_clService servicio = new ServicioVisor.visor_clService();
                ServicioVisor.Request_TT request = new ServicioVisor.Request_TT();
                request.IdentificadorProfesional = "0";
                request.IdentificadorUnicoPaciente = idRyf;
                request.NumeroIdentificacionPaciente = run;
                request.SistemaSolicitaConsulta = 1;
                request.TipoIdentificacionPaciente = 1;
                ServicioVisor.responseTT response = servicio.ObtenerURLVisorHCC(request);
                //hay que sobrescribir la url ya que se usará de forma local
                //https://previsor.saludenred.cl/#/NzA0Nzk1OTE=/MQ==/ODkxMDc2OA==/ACF6B3904AC3317878F7837BE03C7B9F
                //algo asi como 
                /*
                 /#/NzA0Nzk1OTE=/MQ==/ODkxMDc2OA==/ACF6B3904AC3317878F7837BE03C7B9F
                */
                // y dejarlo 
                /*
                /#/NzA0Nzk1OTE=/MQ==/ODkxMDc2OA==/ACF6B3904AC3317878F7837BE03C7B9F/arraydecodigosdeis=12,34,33
                por mientras la dejamos solita 
                */

                //respuesta
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(response.URL);
                httpResponse.Content = new StringContent(JSON);
                httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);


            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;


        }

    }
}