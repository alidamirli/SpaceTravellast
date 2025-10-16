using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpaceTravellast.Models;

namespace spacetravelof.ViewComponents
{
    public class ToursViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using (var a = new RmlubecoSpaceContext())
            {
                ViewBag.Tours = a.Turlars.Include(x => x.TurlarCurrency).OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                return View();
            }
        }
    }
}
