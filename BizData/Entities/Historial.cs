using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizData.Entities
{
    public class Historial
    {
        public int HistorialID { get; set; }
        public string UsuarioId { get; set; }
        public string UsuarioTargetId { get; set; }

        public string Comentario { get; set; }
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario UsuarioTarget { get; set; }
        public virtual Itinerario Itinerario { get; set; }
    }
}
