using Microsoft.AspNetCore.Mvc;
using SpaceTravellast.Models;

namespace SpaceTravellast.Controllers
{
    public class AdminController : Controller
    {
        private readonly RmlubecoSpaceContext _sql;
        public AdminController(RmlubecoSpaceContext sql)
        {
            _sql = sql;
        }




        public IActionResult Messages()
        {


            return View(_sql.Contacts.OrderByDescending(x => x.ContactId).ToList());


        }
    }
}
