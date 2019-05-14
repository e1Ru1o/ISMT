using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BizData.Entities
{
    public class Itinerario
    {
        public int ItinerarioID { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }

        [NotMapped]
        public IEnumerable<Pais> GetPaises
        {
            get
            {
                if (Viajes != null)
                    foreach (var viaje in Viajes)
                        yield return viaje.Pais;
            }
            set { }
        }

        [NotMapped]
        public IEnumerable<Ciudad> GetCiudades { get {
                if (Viajes != null)
                    foreach (var viaje in Viajes)
                        yield return viaje.Ciudad;
            } set { }
        }

        [NotMapped]
        public IEnumerable<Institucion> GetInstituciones
        {
            get
            {
                if (Viajes != null)
                    foreach (var viaje in Viajes)
                        yield return viaje.Institucion;
            }
            set { }
        }
    }
}
