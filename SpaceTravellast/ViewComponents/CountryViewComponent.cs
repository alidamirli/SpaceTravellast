using Microsoft.AspNetCore.Mvc;
using SpaceTravellast.Models;

namespace spacetravelof.ViewComponents
{
    public class CountryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using (var a = new RmlubecoSpaceContext())
            {
                ViewBag.Countries = a.Countries.OrderBy(x => x.CountryName).Take(4).ToList();
                return View();
            }
        }
    }
}
