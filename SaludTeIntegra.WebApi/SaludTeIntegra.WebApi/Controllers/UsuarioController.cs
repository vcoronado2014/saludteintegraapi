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
    public class UsuarioController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.NombreUsuario == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Nombre de Usuario");
            }
            //password no es requerido ya que puede ser una actualizaciòn de usuario
            else if (data.EcolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Ecol Id");
            }
            else if (data.RolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Rol Id");
            }
            else if (data.Nombres == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Nombres");
            }
            else if (data.PrimerApellido == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Primer Apellido");
            }
            //segundo apellido no es requerido
            //run no es requerido
            else if (data.CorreoElectronico == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Correo Electronico");
            }
            //telefono contacto 1 no es requerido
            //telefono contacto 2 no es requerido
            //AusId es el elemento decidor para determinar si es nuevo o modificado
            else if (data.AusId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Aus Id");
            }
            else
            {
                try
                {
                    //variables
                    string nombreUsuario = data.NombreUsuario;
                    string ecolId = data.EcolId;
                    string rolId = data.RolId;
                    string nombres = data.Nombres;
                    string primerApellido = data.PrimerApellido;
                    string correoElectronico = data.CorreoElectronico;
                    string ausId = data.AusId;
                    //variables que son opcionales
                    string password = "";
                    string passwordEncript = "";
                    if (data.Password != null)
                    {
                        password = data.Password;
                        passwordEncript = VCFramework.NegocioMySQL.Utiles.Encriptar(password);

                    }
                    string segundoApellido = "";
                    if (data.SegundoApellido != null)
                    {
                        segundoApellido = data.SegundoApellido;
                    }
                    string telefonoContactoUno = "";
                    if (data.TelefonoContactoUno != null)
                    {
                        telefonoContactoUno = data.TelefonoContactoUno;
                    }
                    string telefonoContactoDos = "";
                    if (data.TelefonoContactoDos != null)
                    {
                        telefonoContactoDos = data.TelefonoContactoDos;
                    }
                    string activo = "1";
                    if (data.Activo != null)
                    {
                        activo = data.Activo;
                    }
                    string eliminado = "0";
                    if (data.Eliminado != null)
                    {
                        eliminado = data.Eliminado;
                    }
                    string run = "";
                    if (data.Run != null)
                    {
                        run = data.Run;
                    }

                    //para controlar si es nuevo o antiguo
                    bool esNuevo = false;
                    int ausIdInt = int.Parse(ausId);
                    if (ausIdInt == 0)
                        esNuevo = true;

                    //Autentificacion usuario
                    VCFramework.Entidad.AutentificacionUsuario ausG = new AutentificacionUsuario();
                    ausG.Activo = int.Parse(activo);
                    ausG.Eliminado = int.Parse(eliminado);
                    ausG.EcolId = int.Parse(ecolId);
                    ausG.NombreUsuario = nombreUsuario;
                    ausG.RolId = int.Parse(rolId);
                    //persona
                    VCFramework.Entidad.Persona personaG = new Persona();
                    personaG.Activo = int.Parse(activo);
                    personaG.ApellidoMaterno = segundoApellido;
                    personaG.ApellidoPaterno = primerApellido;
                    personaG.AusId = int.Parse(ausId);
                    personaG.CorreoElectronico = correoElectronico;
                    personaG.Eliminado = int.Parse(eliminado);
                    personaG.Nombres = nombres;
                    personaG.Run = run;
                    personaG.TelefonoContactoDos = telefonoContactoDos;
                    personaG.TelefonoContactoUno = telefonoContactoUno;
                    //rol
                    VCFramework.Entidad.Roles rolG = VCFramework.NegocioMySql.Roles.ListarRolesPorId(int.Parse(rolId));
                    //retorno de entidad
                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usuario = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();

                    //si el usuario es nuevo
                    if (esNuevo)
                    {
                        //esta insertando
                        //obtenemos por nombre usuario para verificar si ya existe
                        VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorNombreUsuario(nombreUsuario);
                        if (aus != null && aus.Id == 0)
                        {
                            //todo bien, seguir
                            ausG.Password = passwordEncript;
                            ausG.FechaCreacion =VCFramework.NegocioMySQL.Utiles.ConstruyeFechaDos(DateTime.Now);
                            int idAus = VCFramework.NegocioMySql.AutentificacionUsuario.Insertar(ausG);
                            ausG.Id = idAus;
                            int idPer = VCFramework.NegocioMySql.Persona.Insertar(personaG);
                            personaG.Id = idPer;
                            personaG.AusId = idAus;
                            //nuevo elemento a retornar
                            usuario.AutentificacionUsuario = new AutentificacionUsuario();
                            usuario.AutentificacionUsuario = ausG;
                            usuario.Persona = new Persona();
                            usuario.Persona = personaG;
                            usuario.Rol = new Roles();
                            usuario.Rol = rolG;
                            //todo correcto en la creacion
                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usuario, EnumMensajes.Registro_creado_con_exito);

                        }
                        else
                        {
                            //ya existe, error
                            httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Usuario_ya_existe);
                        }

                    }
                    else
                    {
                        //esta modificando hay que buscar a la persona
                        VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorId(int.Parse(ausId));
                        VCFramework.Entidad.Persona per = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(int.Parse(ausId));
                        if (aus != null && aus.Id > 0)
                        {
                            //usuario ya existe esta correcto
                            ausG.Id = aus.Id;
                            ausG.FechaCreacion = aus.FechaCreacion;
                            if (password.Length > 0)
                            {
                                ausG.Password = passwordEncript;
                            }
                            else
                            {
                                ausG.Password = aus.Password;
                            }
                            VCFramework.NegocioMySql.AutentificacionUsuario.Modificar(ausG);

                            personaG.Id = per.Id;
                            personaG.AusId = per.AusId;

                            VCFramework.NegocioMySql.Persona.Modificar(personaG);
                            //nuevo elemento a retornar

                            usuario.AutentificacionUsuario = new AutentificacionUsuario();
                            usuario.AutentificacionUsuario = ausG;
                            usuario.Persona = new Persona();
                            usuario.Persona = personaG;
                            usuario.Rol = new Roles();
                            usuario.Rol = rolG;
                            //todo correcto en la creacion
                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usuario, EnumMensajes.Registro_modificado_con_exito);
                        }
                        else
                        {
                            //usuario no existe
                            httpResponse = ManejoMensajes.RetornaMensajeError(httpResponse, EnumMensajes.Usuario_no_existe);
                        }
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

        //falta agregar la eliminacion y activacion, la cual serà 
        //controlada por un tipo de operacion donde:
        //Tipo 0 = dfesactivacion
        //tipo 1 = activar
        //tipo 2 = eliminar.
        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete([FromUri]string Id, [FromUri]string TipoOperacion)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            //string Input = JsonConvert.SerializeObject(DynamicClass);

            //dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            //este id corresponde al AusId
            if (Id == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Aus Id");
            }
            else if (TipoOperacion == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Tipo Operacion");
            }
            else
            {
                string id = Id;
                string tipoOperacion = TipoOperacion;
                //buscamos 
                VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorId(int.Parse(id));
               

                try
                {
                    if (aus != null && aus.Id > 0)
                    {
                        VCFramework.Entidad.Roles rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(aus.RolId);
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio usu = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        //ya estan seteados los valores asi que se procede a realizar directamente la operacion
                        if (tipoOperacion == "0")
                        {
                            //desactivar
                            VCFramework.NegocioMySql.AutentificacionUsuario.Desactivar(aus);
                            VCFramework.Entidad.Persona persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(int.Parse(id));
                            if (persona != null && persona.Id > 0)
                            {
                                VCFramework.NegocioMySql.Persona.Desactivar(persona);
                            }
                            //data de retorno
                            usu.AutentificacionUsuario = new AutentificacionUsuario();
                            usu.AutentificacionUsuario = aus;
                            usu.Persona = new Persona();
                            usu.Persona = persona;
                            usu.Rol = new Roles();
                            usu.Rol = rol;

                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usu, EnumMensajes.Registro_desactivado_con_exito);
                        }
                        else if (tipoOperacion == "1")
                        {
                            //activar
                            VCFramework.NegocioMySql.AutentificacionUsuario.Activar(aus);
                            VCFramework.Entidad.Persona persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(int.Parse(id));
                            if (persona != null && persona.Id > 0)
                            {
                                VCFramework.NegocioMySql.Persona.Activar(persona);
                            }
                            //data de retorno
                            usu.AutentificacionUsuario = new AutentificacionUsuario();
                            usu.AutentificacionUsuario = aus;
                            usu.Persona = new Persona();
                            usu.Persona = persona;
                            usu.Rol = new Roles();
                            usu.Rol = rol;

                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usu, EnumMensajes.Registro_desactivado_con_exito);
                        }
                        else
                        {
                            //eliminar 
                            VCFramework.NegocioMySql.AutentificacionUsuario.Eliminar(aus);
                            VCFramework.Entidad.Persona persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(int.Parse(id));
                            if (persona != null && persona.Id > 0)
                            {
                                VCFramework.NegocioMySql.Persona.Eliminar(persona);
                            }
                            //data de retorno
                            usu.AutentificacionUsuario = new AutentificacionUsuario();
                            usu.AutentificacionUsuario = aus;
                            usu.Persona = new Persona();
                            usu.Persona = persona;
                            usu.Rol = new Roles();
                            usu.Rol = rol;

                            httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usu, EnumMensajes.Registro_eliminado_con_exito);
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

        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Get([FromUri]string id)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            //validaciones antes de ejecutar la llamada.
            //este id corresponde al AusId
            if (id == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Aus Id");
            }
            else
            {
                try
                {
                    int idBuscar = int.Parse(id);

                    VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorId(idBuscar);
                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usu = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                    if (aus != null && aus.Id > 0)
                    {
                        VCFramework.Entidad.Roles rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(aus.RolId);
                        VCFramework.Entidad.Persona persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(int.Parse(id));
                        
                        //data de retorno
                        usu.AutentificacionUsuario = new AutentificacionUsuario();
                        usu.AutentificacionUsuario = aus;
                        usu.Persona = new Persona();
                        usu.Persona = persona;
                        usu.Rol = new Roles();
                        usu.Rol = rol;
                        VCFramework.Entidad.EntidadContratante contratante = VCFramework.NegocioMySql.EntidadContratante.ListarEcolPorId(aus.EcolId);
                        usu.EntidadContratante = new EntidadContratante();
                        usu.EntidadContratante = contratante;

                        httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usu, EnumMensajes.Registro_desactivado_con_exito);
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

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.EcolId == "")
            {
                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Ecol Id");
            }
            else if (data.RolId == "")
            {

                httpResponse = ManejoMensajes.RetornaMensajeParametroVacio(httpResponse, EnumMensajes.Parametro_vacio_o_invalido, "Rol Id");
            }
            else
            {
                try
                {
                    string ecolId = data.EcolId;
                    string rolId = data.RolId;

                    //buscamos a los usuarios dependiendo de algunos factores
                    //si el rol es Super Administrador
                    List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> usuarios = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    if (int.Parse(rolId) == 1)
                    {
                        //si el valor de nodid = 0 es todos
                        if(int.Parse(ecolId) == 0)
                        {
                            List<VCFramework.Entidad.AutentificacionUsuario> aus = VCFramework.NegocioMySql.AutentificacionUsuario.Listar();
                            if (aus != null && aus.Count > 0)
                            {
                                foreach(VCFramework.Entidad.AutentificacionUsuario au in aus)
                                {
                                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usuEnv = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                                    usuEnv.AutentificacionUsuario = new AutentificacionUsuario();
                                    usuEnv.AutentificacionUsuario = au;
                                    usuEnv.Persona = new Persona();
                                    usuEnv.Persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(au.Id);
                                    usuEnv.Rol = new Roles();
                                    usuEnv.Rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(au.RolId);
                                    VCFramework.Entidad.EntidadContratante contratante = VCFramework.NegocioMySql.EntidadContratante.ListarEcolPorId(au.EcolId);
                                    usuEnv.EntidadContratante = new EntidadContratante();
                                    usuEnv.EntidadContratante = contratante;
                                    usuarios.Add(usuEnv);
                                }
                            }

                        }
                        else
                        {
                            List<VCFramework.Entidad.AutentificacionUsuario> aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorEcolId(int.Parse(ecolId));
                            if (aus != null && aus.Count > 0)
                            {
                                foreach (VCFramework.Entidad.AutentificacionUsuario au in aus)
                                {
                                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usuEnv = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                                    usuEnv.AutentificacionUsuario = new AutentificacionUsuario();
                                    usuEnv.AutentificacionUsuario = au;
                                    usuEnv.Persona = new Persona();
                                    usuEnv.Persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(au.Id);
                                    usuEnv.Rol = new Roles();
                                    usuEnv.Rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(au.RolId);
                                    VCFramework.Entidad.EntidadContratante contratante = VCFramework.NegocioMySql.EntidadContratante.ListarEcolPorId(au.EcolId);
                                    usuEnv.EntidadContratante = new EntidadContratante();
                                    usuEnv.EntidadContratante = contratante;
                                    usuarios.Add(usuEnv);
                                }
                            }
                        }
                    }
                    else
                    {
                        List<VCFramework.Entidad.AutentificacionUsuario> aus = VCFramework.NegocioMySql.AutentificacionUsuario.ListarUsuariosPorEcolId(int.Parse(ecolId));
                        if (aus != null && aus.Count > 0)
                        {
                            foreach (VCFramework.Entidad.AutentificacionUsuario au in aus)
                            {
                                if (au.RolId != 1)
                                {
                                    VCFramework.EntidadFuncional.UsuarioEnvoltorio usuEnv = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                                    usuEnv.AutentificacionUsuario = new AutentificacionUsuario();
                                    usuEnv.AutentificacionUsuario = au;
                                    usuEnv.Persona = new Persona();
                                    usuEnv.Persona = VCFramework.NegocioMySql.Persona.ListarPersonaPorAusId(au.Id);
                                    usuEnv.Rol = new Roles();
                                    usuEnv.Rol = VCFramework.NegocioMySql.Roles.ListarRolesPorId(au.RolId);
                                    VCFramework.Entidad.EntidadContratante contratante = VCFramework.NegocioMySql.EntidadContratante.ListarEcolPorId(au.EcolId);
                                    usuEnv.EntidadContratante = new EntidadContratante();
                                    usuEnv.EntidadContratante = contratante;
                                    usuarios.Add(usuEnv);
                                }
                            }
                        }
                    }

                    httpResponse = ManejoMensajes.RetornaMensajeCorrecto(httpResponse, usuarios);

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