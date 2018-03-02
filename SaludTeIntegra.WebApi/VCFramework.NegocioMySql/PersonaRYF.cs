﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class PersonaRYF
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDRyf"];
        public static VCFramework.EntidadFuncional.Persona ListarPersonaPorRun(string run)
        {
            VCFramework.EntidadFuncional.Persona entidad = new EntidadFuncional.Persona();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "RUN";
            filtro.Valor = run.ToString();
            filtro.TipoDato = TipoDatoGeneral.Varchar;


            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.EntidadFuncional.Persona>(filtros, setCnsWebLun);
            List<VCFramework.EntidadFuncional.Persona> lista2 = new List<VCFramework.EntidadFuncional.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.EntidadFuncional.Persona>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                entidad = lista2[0];
            }
            return entidad;
        }
    }
}
