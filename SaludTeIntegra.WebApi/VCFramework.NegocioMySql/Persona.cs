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

    }
}
