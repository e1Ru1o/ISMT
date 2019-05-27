using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class WelcomeViewModel
    {
        public List<string> UserPendings { get; set; }

        public List<Itinerario> ViajesUpdated { get; set; }

        public int Notifications { get; set; }

        public List<string> NotificationsList { get; set; }

        public List<string> InvitadosPropios { get; set; }

        public List<string> InvitadosAjenos { get; set; }
    }
}
