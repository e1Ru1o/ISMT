using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class ViajeInvitado
    {
        public int ViajeInvitadoID { get; set; }
        public DateTime? FechaLLegada { get; set; }
        public string Procedencia { get; set; }
        public string Nombre { get; set; }
        public string Motivo { get; set; }
        public Estado Estado { get; set; }
        public int Update { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Historial> Historial { get; set; }
    }
}
