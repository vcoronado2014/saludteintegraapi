using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class AutentificacionUsuario : EntidadBase
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EcolId { get; set; }
        public int RolId { get; set; }
    }
}
