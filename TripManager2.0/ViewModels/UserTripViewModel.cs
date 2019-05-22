using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class UserTripViewModel : TripViewModel
    {
        public string Owner { get; set; }
        public int uID { get; set; }
        public UserTripViewModel(DateTime s, DateTime f, string status, int iId, Usuario user)
            : base(s, f, status, iId)
        {
            Owner = user.FirstName + " " + user.SecondName;
        }
    }
}
