using BizData.Entities;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class CiudadCommand : CiudadViewModel
    {
        public Pais Pais { get; set; }

        public CiudadCommand(Pais pais)
        {
            Pais = pais ?? throw new ArgumentNullException(nameof(pais));
        }

        public CiudadCommand()
        {

        }

        public Ciudad ToCiudad()
        {
            return new Ciudad
            {
                Nombre = Nombre,
                Pais = Pais
            };
        }
    }
}
