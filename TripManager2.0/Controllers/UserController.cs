using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using Microsoft.AspNetCore.Mvc;
using TripManager2._0.ViewModels;

namespace TripManager2._0.Controllers
{
    public class UserController : Controller
    {
        private readonly GetterUtils _getterUtils;
        private readonly IUnitOfWork _context;

        public UserController(IUnitOfWork context ,
            IGetterUtils getterUtils)
        {
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
        }

        public IActionResult Welcome(TableUserViewModel user)
        {
            return View(user);
        }

        public IActionResult Print(TableUserViewModel text)
        {
            //GetterAll getter = new GetterAll(_getterUtils, _context);
            //var result = getter.GetAll("Entidad");

            return Content(text.Table.ToString());
        }
    }
}
