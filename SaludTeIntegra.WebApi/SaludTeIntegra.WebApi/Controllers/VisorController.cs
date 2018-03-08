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


            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.Run == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Rol Id");
            }
            else
            {
                string run = data.Run;
                string idRyf = "";
                try
                {

                    //OBTENEMOS EL IDRYF
                    VCFramework.EntidadFuncional.Persona perRyf = new VCFramework.EntidadFuncional.Persona();
                    perRyf = VCFramework.NegocioMySql.PersonaRYF.ListarPersonaPorRun(run);

                    if (perRyf != null && perRyf.Id > 0)
                    {

                        idRyf = perRyf.Id.ToString();

                        ServicioVisor.visor_clService servicio = new ServicioVisor.visor_clService();
                        ServicioVisor.Request_TT request = new ServicioVisor.Request_TT();
                        request.IdentificadorProfesional = "0";
                        request.IdentificadorUnicoPaciente = idRyf;
                        request.NumeroIdentificacionPaciente = run;
                        request.SistemaSolicitaConsulta = 1;
                        request.TipoIdentificacionPaciente = 1;
                        ServicioVisor.responseTT response = servicio.ObtenerURLVisorHCC(request);
                        if (response != null && response.URL.Length > 0)
                        {
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
                            //vamos a procesar la url
                            string[] elementos = response.URL.Split('#');
                            //los sacamos de atrás para adelante, necesitamos los 4 ultimos
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            
                            if (elementos != null && elementos.Length > 1)
                            {
                                sb.Append(elementos[1]);
                            }
                            EntidadUrl entidad = new EntidadUrl();
                            entidad.UrlVisor = response.URL;
                            entidad.UrlHash = sb.ToString();

                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, entidad);

                        }
                        else
                        {
                            httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, 1000, "Error al obtener url de visor");
                        }

                    }
                    else
                    {
                        httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, 1000, "Error en la BD de RYF, usuario no existe, valor nulo");
                    }


                }
                catch (Exception ex)
                {
                    VCFramework.NegocioMySQL.Utiles.NLogs(ex);
                    httpResponse = ManejoMensajes.RetornaMensajeExcepcion(httpResponse, ex);
                }
            }
            return httpResponse;


        }
        public class EntidadUrl
        {
            public string UrlVisor { get; set; }
            public string UrlHash { get; set; }
        }

    }
}