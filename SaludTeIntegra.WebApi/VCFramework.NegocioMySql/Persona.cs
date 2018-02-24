using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class Persona
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static VCFramework.Entidad.Persona ListarPersonaPorAusId(int ausId)
        {
            VCFramework.Entidad.Persona entidad = new Entidad.Persona();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "AUS_ID";
            filtro.Valor = ausId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
        public static VCFramework.Entidad.Persona ListarPersonaPorId(int id)
        {
            VCFramework.Entidad.Persona entidad = new Entidad.Persona();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
        public static int Insertar(VCFramework.Entidad.Persona entidad)
        {
            int retorno = 0;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Insertar<VCFramework.Entidad.Persona>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Delete(VCFramework.Entidad.Persona entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Delete<VCFramework.Entidad.Persona>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Modificar(VCFramework.Entidad.Persona entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Persona>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Eliminar(VCFramework.Entidad.Persona entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Eliminado = 1;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Persona>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Desactivar(VCFramework.Entidad.Persona entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Activo = 0;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.Persona>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }


    }
}
