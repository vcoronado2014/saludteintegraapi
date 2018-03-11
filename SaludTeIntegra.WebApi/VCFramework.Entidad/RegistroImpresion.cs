using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class RegistroImpresion
    {
        public int Id { get; set; }
        public int AusId { get; set; }
        public string Fecha { get; set; }
        public string FechaAtencion { get; set; }
        public string  Run { get; set; }
        public int EcolId { get; set; }
    }
}
