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
        public int ID { get; set; }

        public TripViewModel(DateTime s, DateTime f, string status, int iId)
        {
            Identifier = s.Date.ToString().Split()[0] + " --> " + f.Date.ToString().Split()[0];
            ID = iId;
        }

        public TripViewModel(DateTime s, DateTime f, string status)
        {
            Identifier = s.Date.ToString() + " --> " + f.Date.ToString();
            Status = status;
        }
    }
}
