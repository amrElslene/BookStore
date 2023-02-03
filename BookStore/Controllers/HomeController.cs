using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;
using BookStore.Models;
using BookStore.Models.domins;
using BookStore.Repositories.Abstract;
using BookStore.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;
        private readonly IUserService userService;
        private  DatabaseContext context;
        List<Book> cartbooks = new List<Book>();

        public HomeController(IBookService bservice, IUserService uservice)
        {
            this.bookService = bservice;

            this.userService = uservice;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login( string Username, string password)
        {
            if (userService.Iadmin(Username,password))
            {
                Claim c1 = new Claim(ClaimTypes.Role, "admin");
                ClaimsIdentity ci = new ClaimsIdentity("cooki");
                ci.AddClaim(c1);
                ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                HttpContext.SignInAsync(cp);
                return RedirectToAction("GetAllbooks", "Admin");
            }


            else if (userService.usernameex(Username,true,password))
            {
                Claim c1 = new Claim(ClaimTypes.Name, Username);
                Claim c2 = new Claim(ClaimTypes.Role, "user");
                Claim c3 = new Claim("cartitem", "0");
                ClaimsIdentity ci = new ClaimsIdentity("cooki");
                ci.AddClaim(c1);
                ci.AddClaim(c2);
                ci.AddClaim(c3);
                ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                HttpContext.SignInAsync(cp);
                return RedirectToAction(nameof(usermain));

            }

            return View();
        }
        public IActionResult logout()
        {

            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult notaccs()
        {
            return View();
        }


        public IActionResult creatingaccount()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult creatingaccount(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (userService.usernameex(model.Username,false,""))
            {
                return View(model);
            }
            var result = userService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(login));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        [Authorize(Roles = "user")]
        public IActionResult usermain()
        {
            var data = bookService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult addcart(int id)
        {
            var data = bookService.FindById(id);
            cartbooks data1 = new cartbooks();
            data1.Id = data.Id;
            data1.Title = data.Title;
            data1.Genre = data.Genre;
            data1.Publisher = data.Publisher;
            data1.price = data.price;
            data1.Author = data.Author;
            data1.imgname = data.imgname;
            data1.Isbn = data.Isbn;
            data1.TotalPages = data.TotalPages;

            var model = bookService.Addcart(data1);
            var cc = User.Claims.FirstOrDefault(c => c.Type == "cartitem");
            int count = int.Parse(cc.Value);
            count++;
            Claim c1 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            Claim c2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            Claim c3 = new Claim("cartitem", count.ToString());
            ClaimsIdentity ci = new ClaimsIdentity("cooki");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ci.AddClaim(c3);
            ClaimsPrincipal cp = new ClaimsPrincipal(ci);
            HttpContext.SignInAsync(cp);
            return RedirectToAction(nameof(usermain));

        }
        public IActionResult cart()
        {
            var data = bookService.GetAllcart();
            return View(data);
        }
        public IActionResult donecart()
        {
            bookService.donecart();
            Claim c1 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            Claim c2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            Claim c3 = new Claim("cartitem", "0");
            ClaimsIdentity ci = new ClaimsIdentity("cooki");
            ci.AddClaim(c1);
            ci.AddClaim(c2);
            ci.AddClaim(c3);
            ClaimsPrincipal cp = new ClaimsPrincipal(ci);
            HttpContext.SignInAsync(cp);
            return View();
        }

        [HttpGet]
        public IActionResult item(int id)
        {
            var data = bookService.FindById(id);
            return View(data);
        }

        
    }
}