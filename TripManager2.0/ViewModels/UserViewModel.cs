using BizData.Entities;
using BizLogic.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TripManager2._0.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public PermisoTipo per { get; set; }
        public string permission { get; set; }

        public void SetProperties(RegisterUsuarioCommand cmd)
        {
            FirstName = cmd.FirstName;
            SecondName = cmd.SecondName;
            Email = cmd.Email;
        }

        public void SetViewModel(Usuario u)
        {
            FirstName = u.FirstName;
            SecondName = u.SecondName;
            Email = u.Email;
        }

        public void SetPermissions(IEnumerable<Claim> claims)
        {
            var id = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
            id.Sort();
            permission = id[0];
        }
    }
}
