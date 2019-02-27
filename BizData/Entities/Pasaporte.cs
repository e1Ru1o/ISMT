using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public enum PasaporteTipo
    {
        Europeo,Americano
    }

    public class Pasaporte
    {
        public int PasaporteID { get; set; }
        public int UsuarioID { get; set; }
        public long UsuarioCI { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Actualizaciones { get; set; }
        public PasaporteTipo Tipo { get; set; }

        public Usuario Usuario { get; set; }
        public virtual ICollection<Pasaporte_Visa> Visas { get; set; }
    }
}
