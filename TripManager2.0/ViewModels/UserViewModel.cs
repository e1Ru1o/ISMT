using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public PermisoTipo per { get; set; }
    }
}
