using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class ParametrosEcol
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];


        public static VCFramework.Entidad.ParametrosEcol ListarPorEcolId(int ecolId)
        {
            VCFramework.Entidad.ParametrosEcol entidad = new Entidad.ParametrosEcol();
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

            List<object> lista = fac.Leer<VCFramework.Entidad.ParametrosEcol>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.ParametrosEcol> lista2 = new List<VCFramework.Entidad.ParametrosEcol>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.ParametrosEcol>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
        public static int Insertar(VCFramework.Entidad.ParametrosEcol entidad)
        {
            int retorno = 0;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Insertar<VCFramework.Entidad.ParametrosEcol>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Delete(VCFramework.Entidad.ParametrosEcol entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Delete<VCFramework.Entidad.ParametrosEcol>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Modificar(VCFramework.Entidad.ParametrosEcol entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.ParametrosEcol>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Eliminar(VCFramework.Entidad.ParametrosEcol entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Eliminado = 1;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.ParametrosEcol>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Desactivar(VCFramework.Entidad.ParametrosEcol entidad)
        {
            int retorno = 1;
            try
            {
                entidad.Activo = 0;
                Factory fac = new Factory();
                retorno = fac.Update<VCFramework.Entidad.ParametrosEcol>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }

    }
}
