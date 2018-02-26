using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class EntidadContratante : EntidadBase
    {
        public string RazonSocial { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaInicioContrato { get; set; }
        public int RegId { get; set; }
        public int ComId { get; set; }
        public string Direccion { get; set; }
        public string RestoDireccion { get; set; }
        public string NumeroDireccion { get; set; }


    }
}
