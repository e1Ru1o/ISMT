using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class TripViewModel
    {
        public string Identifier { get; set; }
        public string Status { get; set; }

        public TripViewModel(DateTime s, DateTime f, string status)
        {
            Identifier = s.Date.ToString() + " --> " + f.Date.ToString();
            Status = status;
        }
    }
}
