using BookStore.Models.domins;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IBookService bookService;
        private readonly IUserService userService;


        public AdminController(IBookService bservice, IUserService uservice)
        {
            this.bookService = bservice;

            this.userService = uservice;

        }

      

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Book model,IFormFile imgsrc)
        {
            if (imgsrc != null)
            {
                
                string imgname = bookService.getlastid() + "." + imgsrc.FileName.Split(".")[1];
                using (var obj = new FileStream(@".\wwwroot\imgs\bookimgs\" + imgname, FileMode.Create))
                {
                    imgsrc.CopyTo(obj);
                }
                model.imgname = imgname;
            }
            
            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record = bookService.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Update(model);
            if (result)
            {
                return RedirectToAction("GetAllbooks");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Deletebook(int id)
        {

            var result = bookService.Delete(id);
            return RedirectToAction("GetAllbooks");
        }
        public IActionResult Deleteuser(int id)
        {

            var result = userService.Delete(id);
            return RedirectToAction("GetAllusers");
        }

        public IActionResult GetAllbooks()
        {

            var data = bookService.GetAll();
            return View(data);
        }
        public IActionResult GetAllusers()
        {

            var data = userService.GetAll();
            return View(data);
        }
    }
}
