using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class RegistroImpresion
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static int Insertar(VCFramework.Entidad.RegistroImpresion entidad)
        {
            int retorno = 0;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Insertar<VCFramework.Entidad.RegistroImpresion>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
        public static int Delete(VCFramework.Entidad.RegistroImpresion entidad)
        {
            int retorno = 1;
            try
            {
                Factory fac = new Factory();
                retorno = fac.Delete<VCFramework.Entidad.RegistroImpresion>(entidad, setCnsWebLun);
            }
            catch (Exception ex)
            {
                NegocioMySQL.Utiles.NLogs(ex);
            }
            return retorno;
        }
    }
}
