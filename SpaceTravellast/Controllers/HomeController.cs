using ImageMagick;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SpaceTravellast.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;

namespace SpaceTravellast.Controllers
{
    public class HomeController : Controller
    {
        private readonly RmlubecoSpaceContext _spaceTravelContext;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        public HomeController(RmlubecoSpaceContext RmlubecoSpaceContext, IHtmlLocalizer<HomeController> localizer)
        {
            _spaceTravelContext = RmlubecoSpaceContext;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.News = _spaceTravelContext.News.ToList();
            //var a = _spaceTravelContext.Turlars.ToList();
            ViewBag.turlar = _spaceTravelContext.Turlars.Include(x => x.TurlarCurrency).Include(x => x.TurlarCategory).Include(x => x.TurlarCountry).ToList();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {




            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                //contact.ContactDate = DateTime.Now;

                _spaceTravelContext.Contacts.Add(contact);
                _spaceTravelContext.SaveChanges();

                return View();
            }

            return View(contact);
        }

        [HttpPost]
        public IActionResult SendMessage(string SubscriberUsername, string SubscriberEmail, string SubscriberTitle, string SubscriberDescription)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("");
            email.To.Add(MailboxAddress.Parse(""));
            email.Subject = "";
            var builder = new BodyBuilder();
            builder.TextBody = "";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.connect("", 0, securesocketoptions.starttls);
            return RedirectToAction("Contact");
        }


        public IActionResult Services()
        {
            return View();
        }

        public IActionResult News(int page = 0)
        {
            ViewBag.RecentNews = _spaceTravelContext.News.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            var a = _spaceTravelContext.News.Skip(page * 5).Take(5).ToList();
            ViewBag.NewsPageCount = Math.Ceiling((decimal)_spaceTravelContext.News.Count() / 5);
            return View(a);
        }
        public IActionResult NewsDetails(int id)
        {
            ViewBag.RecentNews = _spaceTravelContext.News.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            ViewBag.CommentNews = _spaceTravelContext.Comments.Where(x => x.CommentNewsId == id).ToList();
            return View(_spaceTravelContext.News.SingleOrDefault(x => x.NewsId == id));
        }

        public IActionResult Tours(int page = 0)
        {
            var a = _spaceTravelContext.Turlars.Include(x => x.TurlarCategory).Include(x => x.TurlarCountry).Include(x => x.TurlarCurrency).Skip(page * 4).Take(4).ToList();
            ViewBag.tourcategory = _spaceTravelContext.Categories.ToList();
            ViewBag.ToursPageCount = Math.Ceiling((decimal)_spaceTravelContext.Turlars.Count() / 4);
            return View(a);
        }
        public IActionResult Hotels(int page = 0)
        {
            ViewBag.City = _spaceTravelContext.Cities.ToList();
            ViewBag.HotelPageCount = Math.Ceiling((decimal)_spaceTravelContext.Hotels.Count() / 6);
            return View(_spaceTravelContext.Hotels.Include(x => x.HotelCurrency).Include(x => x.HotelCity).Skip(page * 6).Take(6).ToList());
        }
        public IActionResult HotelsDetails(int id)
        {
            var a = _spaceTravelContext.Hotels.SingleOrDefault(x => x.HotelId == id);
            ViewBag.HotelsPhotos = _spaceTravelContext.Hotels.Where(x => x.HotelId == id).ToList();
            ViewBag.Comment = _spaceTravelContext.Comments.Where(x => x.CommentHotelId == id).ToList();
            ViewBag.Similarhotels = _spaceTravelContext.Hotels.Include(x => x.HotelCity).Where(x => x.HotelCityId == a.HotelCityId).ToList();
            return View(a);
        }
        public IActionResult TourDetails(int id)
        {
            ViewBag.Currency = _spaceTravelContext.Currencies.ToList();
            ViewBag.TurlarFirstPhoto = _spaceTravelContext.Turlars.Where(x => x.TurlarId == id).ToList();
            ViewBag.TurlarPhoto = _spaceTravelContext.Photos.Where(x => x.PhotoId == id).ToList();
            ViewBag.Comments = _spaceTravelContext.Comments.Where(x => x.CommentTurlarId == id).ToList();
            var b = _spaceTravelContext.Turlars.FirstOrDefault(x => x.TurlarId == id);
            ViewBag.SimilarTours = _spaceTravelContext.Turlars.Include(x => x.TurlarCategory).Where(x => x.TurlarCategoryId == b.TurlarCategoryId).ToList();
            return View(b);
        }


        [HttpPost]
        public IActionResult Comments(Comment comment, int htlid)
        {
            comment.CommentHotelId = htlid;
            _spaceTravelContext.Comments.Add(comment);
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("HotelsDetails", "Home", new { id = htlid });
        }
        [HttpPost]
        public IActionResult CommentsNews(Comment comment, int Nwsid)
        {
            comment.CommentNewsId = Nwsid;
            _spaceTravelContext.Comments.Add(comment);
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("NewsDetails", "Home", new { id = Nwsid });
        }


        public class HotelList
        {
            public int HotelId { get; set; }
            public string HotelName { get; set; }
            public decimal? HotelPrice { get; set; }
            public string CityName { get; set; }
            public string HotelPhoto { get; set; }
        }


        public IActionResult Search(int? minprice, int? maxprice, int? countryid)
        {
            List<HotelList> hotelList;
            using (var item = new RmlubecoSpaceContext())
            {
                var data = item.Hotels.Include(x => x.HotelCity).Where(x => 1 == 1);
                if (countryid != null)
                {
                    data = data.Where(x => x.HotelCity.CityId == countryid);
                }
                if (minprice != null)
                {
                    data = data.Where(x => x.HotelPrice >= minprice);
                }
                if (maxprice != null)
                {
                    data = data.Where(x => x.HotelPrice <= maxprice);
                }

                hotelList = data.Select(x => new HotelList
                {
                    HotelId = x.HotelId,
                    HotelName = x.HotelName,
                    HotelPrice = x.HotelPrice,
                    HotelPhoto = x.HotelPhoto,
                    CityName = x.HotelCity.CityName
                }).ToList();
            }

            return Ok(hotelList);
        }


        public IActionResult Edit(int id)
        {
            ViewBag.City = _spaceTravelContext.Cities.ToList();
            ViewBag.Currency = _spaceTravelContext.Currencies.ToList();
            ViewBag.Category = new SelectList(_spaceTravelContext.Categories.ToList(), "CategoryId", "CategoryName");
            var edit = _spaceTravelContext.Turlars.Include(x => x.TurlarCurrency).SingleOrDefault(x => x.TurlarId == id);
            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(int id, Turlar turlar, IFormFile[] sekil, IFormFile FirstPhoto, Currency? currencyName)
        {

            var oldedit = _spaceTravelContext.Turlars.SingleOrDefault(x => x.TurlarId == id);
            var old2edit = _spaceTravelContext.Photos.Where(x => x.PhotoTurlarId == id);
            if (sekil != null)
            {
                _spaceTravelContext.RemoveRange(old2edit);

                foreach (var item in sekil)
                {
                    string photos = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(item.FileName);
                    using (Stream stream = new FileStream("wwwroot/images/" + photos, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }
                    using (IMagickImage toureditmagickImage = new MagickImage("wwwroot/images/" + photos))
                    {
                        toureditmagickImage.Quality = 25;
                        toureditmagickImage.Write("wwwroot/images/Small/" + photos);
                    }

                    Photo photo1 = new Photo();
                    photo1.PhotoName = photos;
                    photo1.PhotoTurlarId = id;
                    _spaceTravelContext.Photos.Add(photo1);
                    _spaceTravelContext.SaveChanges();
                }
            }
            if (FirstPhoto != null)
            {
                string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(FirstPhoto.FileName);
                using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
                {
                    FirstPhoto.CopyTo(stream);
                }
                using (IMagickImage hoteleditmagickImage = new MagickImage("wwwroot/images/" + photoname))
                {
                    hoteleditmagickImage.Quality = 25;
                    hoteleditmagickImage.Write("wwwroot/images/Small/" + photoname);
                }
                oldedit.TurlarFirstphoto = photoname;
            }
            
            oldedit.TurPrice = turlar.TurPrice;
            oldedit.TurTarix = turlar.TurTarix;
            oldedit.TurAd = turlar.TurAd;
            oldedit.TurDescription = turlar.TurDescription;
            oldedit.TurlarCategoryId = turlar.TurlarCategoryId;
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("Tours");
        }
        public IActionResult EditHotels(int id)
        {
            ViewBag.City = _spaceTravelContext.Cities.ToList();
            ViewBag.Currency = _spaceTravelContext.Currencies.ToList();
            var edit = _spaceTravelContext.Hotels.Include(x => x.HotelCurrency).Include(x => x.HotelCity).SingleOrDefault(x => x.HotelId == id);
            return View(edit);
        }
        [HttpPost]
        public IActionResult Edithotels(int id, Hotel hotel, IFormFile Photo)
        {

            var oldedit = _spaceTravelContext.Hotels.SingleOrDefault(x => x.HotelId == id);
            if (Photo != null)
            {
                string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(Photo.FileName);
                using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
                oldedit.HotelPhoto = photoname;
                using (IMagickImage hoteleditmagickImage = new MagickImage("wwwroot/images/" + photoname))
                {
                    hoteleditmagickImage.Quality = 25;
                    hoteleditmagickImage.Write("wwwroot/images/Small/" + photoname);
                }
            }
            oldedit.HotelCity = hotel.HotelCity;
            oldedit.HotelCurrency = hotel.HotelCurrency;
            oldedit.HotelMapLocation = hotel.HotelMapLocation;
            oldedit.HotelStars = hotel.HotelStars;
            oldedit.HotelPrice = hotel.HotelPrice;
            oldedit.HotelName = hotel.HotelName;
            oldedit.HotelDescription = hotel.HotelDescription;
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("Hotels");
        }
        public IActionResult EditNews(int id)
        {
            var edit = _spaceTravelContext.News.SingleOrDefault(x => x.NewsId == id);
            return View(edit);
        }
        [HttpPost]
        public IActionResult EditNews(int id, News news, IFormFile Photo)
        {
            var oldedit = _spaceTravelContext.News.SingleOrDefault(x => x.NewsId == id);

            if (Photo != null)
            {
                string photoname = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(Photo.FileName);
                using (Stream stream = new FileStream("wwwroot/images/" + photoname, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
                oldedit.NewsPhoto = photoname;
                using (IMagickImage newsefitmagickImage = new MagickImage("wwwroot/images/" + photoname))
                {
                    newsefitmagickImage.Quality = 25;
                    newsefitmagickImage.Write("wwwroot/images/Small/" + photoname);
                }
            }
            oldedit.NewsName = news.NewsName;
            oldedit.NewsTime = news.NewsTime;
            oldedit.NewsDescription = news.NewsDescription;
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("News");
        }
        public IActionResult Delete(int id)
        {
            var tur = _spaceTravelContext.Turlars.SingleOrDefault(x => x.TurlarId == id);
            var pht = _spaceTravelContext.Photos.Where(x => x.PhotoTurlarId == id);
            _spaceTravelContext.Photos.RemoveRange(pht);
            _spaceTravelContext.Turlars.RemoveRange(tur);
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("Tours");
        }
        public IActionResult DeleteHotels(int id)
        {
            var hotel = _spaceTravelContext.Hotels.SingleOrDefault(x => x.HotelId == id);
            var cht = _spaceTravelContext.Photos.Where(x => x.PhotoHotelId == id);
            _spaceTravelContext.Hotels.RemoveRange(hotel);
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("Hotels");
        }
        public IActionResult DeleteNews(int id)
        {
            var News = _spaceTravelContext.News.SingleOrDefault(x => x.NewsId == id);
            _spaceTravelContext.News.RemoveRange(News);
            _spaceTravelContext.SaveChanges();
            return RedirectToAction("News");
        }
        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }
    }
}