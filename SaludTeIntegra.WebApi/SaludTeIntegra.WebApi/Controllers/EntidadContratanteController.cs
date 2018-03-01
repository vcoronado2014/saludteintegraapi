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
    public class EntidadContratanteController : ApiController
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

            if (data.RolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Rol Id");
            }
            else if (data.EcolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Ecol Id");
            }
            else
            {
                try
                {
                    string rolId = data.RolId;
                    string ecolId = data.EcolId;
                    int idBuscar = int.Parse(rolId);

                    List<VCFramework.Entidad.EntidadContratante> contratantes = new List<VCFramework.Entidad.EntidadContratante>();

                    if (idBuscar == 1)
                    {
                        //super administrador
                        contratantes = VCFramework.NegocioMySql.EntidadContratante.ListarTodos();
                    }
                    else
                    {
                        //todos sin el super
                        contratantes = VCFramework.NegocioMySql.EntidadContratante.ListarTodos().FindAll(p=>p.Id == int.Parse(ecolId));
                    }

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, contratantes);


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