using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.Administration
{
    public class VisaCommand : VisaViewModel
    {
        public IEnumerable<Pais> Paises { get; set; }
        public IEnumerable<Region> Regiones { get; set; }

        public IEnumerable<Pais_Visa> PaisesVisas { get; set; }
        public IEnumerable<Region_Visa> RegionesVisas { get; set; }

        public VisaCommand(IEnumerable<Pais> paises, IEnumerable<Region> regiones)
        {
            Paises = paises;
            Regiones = regiones;
        }

        public Visa ToVisa()
        {
            return new Visa()
            {
                Name = Nombre,
                Paises = new List<Pais_Visa>(PaisesVisas),
                Regiones = new List<Region_Visa>(RegionesVisas)
            };
        }

        /// <summary>
        /// Gets all the countries that don't have any relationship with a Visa with name visaName.
        /// </summary>
        /// <param name="visaName">The name of that Visa.</param>
        /// <param name="pvHelper">All the Pais_Visa objects in the database.</param>
        /// <param name="paises">All the Pais objects in the database.</param>
        /// <returns></returns>
        public IEnumerable<Pais> GetPaisesWithoutVisa(string visaName, IEnumerable<Pais_Visa> pvHelper, IEnumerable<Pais> paises)
        {
            var paises_visas = pvHelper.Where(pv => pv.Visa.Name != visaName).Select(pv => pv.Pais);

            foreach (var p in paises)
            {
                if (!paises_visas.Where(pv => pv.Nombre == p.Nombre).Any())
                    yield return p;
            }
        }

        /// <summary>
        /// Gets all the countries that belongs to the Region with name regionName.
        /// </summary>
        /// <param name="regionName">Name of region selected.</param>
        /// <param name="regiones">All the Region objects in the database.</param>
        /// <returns></returns>
        public IEnumerable<Pais> GetPaisesFromRegion(string regionName, IEnumerable<Region> regiones)
        {
            return regiones.Where(r => r.Nombre == regionName).Select(r => r.Paises).Single();
        }
    }
}
