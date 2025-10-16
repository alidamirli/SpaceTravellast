using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceTravellast.Models;
using System.Security.Claims;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;

namespace spacetravelof.Controllers
{
    public class UserController : Controller
    {
        private readonly RmlubecoSpaceContext _sql;
        public UserController(RmlubecoSpaceContext sql)
        {
            _sql = sql;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var getUser = _sql.Users.Include(x => x.UserStatus).SingleOrDefault(x => x.UserName == user.UserName && x.UserPassword == user.UserPassword);
            if (getUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, getUser.UserName),
                    new Claim("Id", getUser.UserId.ToString()),
                    new Claim(ClaimTypes.Role, getUser.UserStatus.StatusName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var princital = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princital, props).Wait();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AddHotels()
        {
            ViewBag.Currency = _sql.Currencies.ToList();
            ViewBag.City = _sql.Cities.ToList();
            return View();
        }
        public IActionResult AddHotels(Hotel hotel, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Currency = _sql.Currencies.ToList();
                ViewBag.City = _sql.Cities.ToList();
                return View(hotel);
            }
            if (photo == null)
            {
                ViewBag.Currency = _sql.Currencies.ToList();
                ViewBag.City = _sql.Cities.ToList();
                return View(hotel);
            }
            string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(photo.FileName);
            using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            using (IMagickImage magickImage = new MagickImage("wwwroot/images/" + photoname))
            {
                magickImage.Quality = 25;
                magickImage.Write("wwwroot/images/Small/" + photoname);
            }
            hotel.HotelPhoto = photoname;
            _sql.Hotels.Add(hotel);
            _sql.SaveChanges();
            return RedirectToAction("Hotels", "Home");
        }
        public IActionResult AddTours()
        {
            ViewBag.Country = _sql.Countries.ToList();
            ViewBag.Currency = _sql.Currencies.ToList();
            ViewBag.Category = _sql.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddTours(Turlar turlar, IFormFile[] photo, IFormFile ilksekil)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Currency = _sql.Currencies.ToList();
                ViewBag.Category = _sql.Categories.ToList();
                return View(turlar);
            }
            if (photo == null || ilksekil == null)
            {
                ViewBag.Category = _sql.Categories.ToList();
                return View(turlar);
            }
            string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(ilksekil.FileName);
            using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
            {
                ilksekil.CopyTo(stream);
            }
            using (IMagickImage hotelmagickImage = new MagickImage("wwwroot/images/" + photoname))
            {
                hotelmagickImage.Quality = 25;
                hotelmagickImage.Write("wwwroot/images/Small/" + photoname);
            }
            turlar.TurlarFirstphoto = photoname;
            _sql.Turlars.Add(turlar);
            _sql.SaveChanges();
            foreach (var item in photo)
            {
                string photos = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(item.FileName);
                using (Stream stream = new FileStream("wwwroot/images/" + photos, FileMode.Create))
                {
                    item.CopyTo(stream);
                }
                Photo photo1 = new Photo();
                photo1.PhotoName = photos;
                photo1.PhotoTurlarId = turlar.TurlarId;
                _sql.Photos.Add(photo1);
                _sql.SaveChanges();
            }
            return RedirectToAction("Tours", "Home");
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNews(News news, IFormFile photo)
        {

            if (!ModelState.IsValid)
            {
                return View(news);
            }
            if (photo == null)
            {
                return View(news);
            }
            string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(photo.FileName);
            using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            using (IMagickImage NewsmagickImage = new MagickImage("wwwroot/images/" + photoname))
            {
                NewsmagickImage.Quality = 25;
                NewsmagickImage.Write("wwwroot/images/Small/" + photoname);
            }
            news.NewsPhoto = photoname;
            _sql.News.Add(news);
            _sql.SaveChanges();
            return RedirectToAction("News", "Home");

        }
        

        







    }
}
