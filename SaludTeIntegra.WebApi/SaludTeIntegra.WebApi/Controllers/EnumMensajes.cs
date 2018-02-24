﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaludTeIntegra.WebApi.Controllers
{
    public enum EnumMensajes
    {
        Correcto = 0,
        Excepcion = 1000,
        Usuario_no_existe = 1,
        Clave_incorrecta = 2,
        Sin_persona_asociada = 3,
        Sin_Rol_asociado = 4,
        Inactivo_o_Eliminado = 5,
        Parametro_vacio_o_invalido = 6
    }
}