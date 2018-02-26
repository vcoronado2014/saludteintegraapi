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

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.usuario == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Nombre de Usuario");
            }
            else if (data.password == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Password");
            }
            else
            {
                try
                {
                    string usu = data.usuario;
                    string pass = data.password;

                    string password = VCFramework.NegocioMySQL.Utiles.Encriptar(pass);

                    VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorNombreUsuario(usu);
                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usuario = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                    if (aus != null && aus.Id > 0)
                    {
                        //verificamos eliminado y activo
                        if (aus.Activo == 1 && aus.Eliminado == 0)
                        {
                            //ahora comparamos la clave
                            if (aus.Password == password)
                            {
                                //buscamos persona
                                VCFramework.Entidad.Persona persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(aus.Id);
                                if (persona != null && persona.Id > 0)
                                {
                                    //buscamos rol
                                    VCFramework.Entidad.Roles rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(aus.RolId);
                                    if (rol != null && rol.Id > 0)
                                    {
                                        //ahora esta todo ok y construimos la data respectiva
                                        usuario.AutentificacionUsuario = new AutentificacionUsuario();
                                        usuario.AutentificacionUsuario = aus;
                                        usuario.Persona = new Persona();
                                        usuario.Persona = persona;
                                        usuario.Rol = new Roles();
                                        usuario.Rol = rol;
                                        VCFramework.Entidad.EntidadContratante contratante = VCFramework.NegocioMySql.EntidadContratante.ListarEcolPorId(aus.EcolId);
                                        usuario.EntidadContratante = new EntidadContratante();
                                        usuario.EntidadContratante = contratante;

                                        httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usuario);

                                    }
                                    else
                                    {
                                        httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Sin_Rol_asociado);
                                    }
                                }
                                else
                                {
                                    httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Sin_persona_asociada);
                                }
                            }
                            else
                            {
                                httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Clave_incorrecta);
                            }
                        }
                        else
                        {
                            //autentificacion inactiva o eliminada
                            httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Inactivo_o_Eliminado);
                        }
                    }
                    else
                    {
                        httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Usuario_no_existe);
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


    }
}