using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class ViajeViewModel
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<string> paises { get; set;}

        public ViajeViewModel()
        {
            start = DateTime.Now;
            end = DateTime.Now;
            paises = new List<string>();
        }
    }
}
