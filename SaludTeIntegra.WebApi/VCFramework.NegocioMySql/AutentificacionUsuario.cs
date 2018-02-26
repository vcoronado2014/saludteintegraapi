using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class AutentificacionUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.AutentificacionUsuario> Listar()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            return lista2;
        }

        public static List<VCFramework.Entidad.AutentificacionUsuario> ListarUsuariosPorEcolId(int ecolId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ECOL_ID";
            filtro.Valor = ecolId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtroEliminado = new FiltroGenerico();
            filtroEliminado.Campo = "ELIMINADO";
            filtroEliminado.Valor = "0";
            filtroEliminado.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtroEliminado);

            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            return lista2;
        }
        public static VCFramework.Entidad.AutentificacionUsuario ListarUsuariosPorNombreUsuario(string nombreUsuario)
        {
            VCFramework.Entidad.AutentificacionUsuario entidad = new Entidad.AutentificacionUsuario();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "NOMBRE_USUARIO";
            filtro.Valor = nombreUsuario.ToString();
            filtro.TipoDato = TipoDatoGeneral.Varchar;

            FiltroGenerico filtroEliminado = new FiltroGenerico();
            filtroEliminado.Campo = "ELIMINADO";
            filtroEliminado.Valor = "0";
            filtroEliminado.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtroEliminado);

            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
        public static VCFramework.Entidad.AutentificacionUsuario ListarUsuariosPorId(int id)
        {
            VCFramework.Entidad.AutentificacionUsuario entidad = new Entidad.AutentificacionUsuario();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtroEliminado = new FiltroGenerico();
            filtroEliminado.Campo = "ELIMINADO";
            filtroEliminado.Valor = "0";
            filtroEliminado.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtroEliminado);

            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }

        public static int Insertar(VCFramework.Entidad.AutentificacionUsuario entidad)
        {
            int retorno = 0;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Insertar<VCFramework.Entidad.AutentificacionUsuario>(entidad, setCnsWebLun);
            }
            catch(Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Delete(VCFramework.Entidad.AutentificacionUsuario entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Delete<VCFramework.Entidad.AutentificacionUsuario>(entidad, setCnsWebLun);
            }
            catch(Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Modificar(VCFramework.Entidad.AutentificacionUsuario entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.AutentificacionUsuario>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Eliminar(VCFramework.Entidad.AutentificacionUsuario entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Eliminado = 1;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.AutentificacionUsuario>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Desactivar(VCFramework.Entidad.AutentificacionUsuario entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Activo = 0;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.AutentificacionUsuario>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }

    }
}
