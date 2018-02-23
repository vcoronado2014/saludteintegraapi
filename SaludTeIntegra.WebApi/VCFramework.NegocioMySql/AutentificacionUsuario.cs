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

        public static List<VCFramework.Entidad.AutentificacionUsuario> ListarUsuariosPorNodId(int nodId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "NOD_ID";
            filtro.Valor = nodId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;
            
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.AutentificacionUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.AutentificacionUsuario> lista2 = new List<VCFramework.Entidad.AutentificacionUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.AutentificacionUsuario>().ToList();
            }
            return lista2;
        }

    }
}
