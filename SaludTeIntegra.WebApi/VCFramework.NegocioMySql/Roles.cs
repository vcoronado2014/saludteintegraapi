using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class Roles
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static VCFramework.Entidad.Roles ListarRolesPorId(int id)
        {
            VCFramework.Entidad.Roles entidad = new Entidad.Roles();
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

            List<object> lista = fac.Leer<VCFramework.Entidad.Roles>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.Roles> lista2 = new List<VCFramework.Entidad.Roles>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Roles>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
        public static List<VCFramework.Entidad.Roles> ListarSinSuper()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<object> lista = fac.Leer<VCFramework.Entidad.Roles>(setCnsWebLun);
            List<VCFramework.Entidad.Roles> lista2 = new List<VCFramework.Entidad.Roles>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Roles>().ToList();
            }
            if (lista2 != null && lista2.Count > 0)
            {
                lista2 = lista2.FindAll(p => p.Id != 1 && p.Eliminado == 0);
            }
            return lista2;
        }
        public static List<VCFramework.Entidad.Roles> ListarTodos()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<object> lista = fac.Leer<VCFramework.Entidad.Roles>(setCnsWebLun);
            List<VCFramework.Entidad.Roles> lista2 = new List<VCFramework.Entidad.Roles>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Roles>().ToList();
            }
            if (lista2 != null && lista2.Count > 0)
            {
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            }
            return lista2;
        }
        public static int Insertar(VCFramework.Entidad.Roles entidad)
        {
            int retorno = 0;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Insertar<VCFramework.Entidad.Roles>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Delete(VCFramework.Entidad.Roles entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Delete<VCFramework.Entidad.Roles>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Modificar(VCFramework.Entidad.Roles entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Roles>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Eliminar(VCFramework.Entidad.Roles entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Eliminado = 1;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Roles>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Desactivar(VCFramework.Entidad.Roles entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Activo = 0;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Roles>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }

    }
}
