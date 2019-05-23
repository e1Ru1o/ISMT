using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class ViajeViewModel
    {
        public List<DateTime?> Start { get; set; }
        public List<DateTime?> End { get; set; }
        public List<string> Country { get; set;}
        public List<string> City { get; set; }
        public List<string> Motivo { get; set; }
        public List<string> Posibilities { get; set; }

        public ViajeViewModel()
        {
            Start = new List<DateTime?>();
            End = new List<DateTime?>();
            City = new List<string>();
            Country = new List<string>();
            Motivo = new List<string>();
            Posibilities = new List<string>();
    	}
    }
}
