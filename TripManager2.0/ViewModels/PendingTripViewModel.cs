using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class PendingTripViewModel
    {
        public IEnumerable<TripViewModel> Users { get; set; }
        public IEnumerable<InvitationViewModel> Visitants { get; set; }
    }

    public class PendingUserTripViewModel
    {
        public IEnumerable<UserTripViewModel> Users { get; set; }
        public IEnumerable<InvitationViewModel> Visitants { get; set; }
    }
}
