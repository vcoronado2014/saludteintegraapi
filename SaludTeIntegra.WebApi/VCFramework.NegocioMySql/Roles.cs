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

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

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

    }
}
