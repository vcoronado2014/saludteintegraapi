using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class ParametrosEcol : EntidadBase
    {
        public int EcolId { get; set; }
        public int EnviaCorreo { get; set; }
        public string LogoUno { get; set; }
        public string LogoDos { get; set; }
        public int MuestraLogoUno { get; set; }
        public int MuestraLogoDos { get; set; }
        public int TopeUsuarios { get; set; }
        public string FechaTerminoContrato { get; set; }

    }
}
