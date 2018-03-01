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
    public class RolController : ApiController
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
            else
            {
                try
                {
                    string rolId = data.RolId;
                    int idBuscar = int.Parse(rolId);

                    List<VCFramework.Entidad.Roles> roles = new List<VCFramework.Entidad.Roles>();

                    if (idBuscar == 1)
                    {
                        //super administrador
                        roles = VCFramework.NegocioMySql.Roles.ListarTodos();
                    }
                    else
                    {
                        //todos sin el super
                        roles = VCFramework.NegocioMySql.Roles.ListarSinSuper();
                    }

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, roles);


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