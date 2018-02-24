using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class Persona : EntidadBase
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Run { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoContactoUno { get; set; }
        public string TelefonoContactoDos { get; set; }
        public int AusId { get; set; }

    }
}
