using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizLogic.Administration;

namespace TripManager2._0.ViewModels
{
    public class EditVisaViewModel : VisaViewModel
    {
        public int id { get; set; }
        public IEnumerable<string> SelectedPais { get; set; }
        public IEnumerable<string> SelectedRegion { get; set; }
    }
}
