using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class ViajeViewModel
    {
        public List<DateTime> start { get; set; }
        public List<DateTime> end { get; set; }
        public List<string> country { get; set;}
        public List<string> city { get; set; }
        public List<string> mot { get; set; }

        public ViajeViewModel()
        {
            start = new List<DateTime>();
            end = new List<DateTime>();
            city = new List<string>();
            country = new List<string>();
            mot = new List<string>();
    }
    }
}
