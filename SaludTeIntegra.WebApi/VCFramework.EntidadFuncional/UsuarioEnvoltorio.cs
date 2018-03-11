using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class UsuarioEnvoltorio
    {
        public Entidad.AutentificacionUsuario AutentificacionUsuario { get; set; }
        public Entidad.Persona Persona { get; set; }
        public Entidad.Roles Rol { get; set; }
        public Entidad.EntidadContratante EntidadContratante { get; set; }
        public Entidad.ParametrosEcol ParametrosEcol { get; set; }
    }
}
