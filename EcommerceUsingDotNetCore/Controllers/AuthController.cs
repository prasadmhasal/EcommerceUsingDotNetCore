using EcommerceUsingDotNetCore.Context;
using EcommerceUsingDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;
        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }


        [AcceptVerbs("Post","Get")]
        public IActionResult CheckExistingEmailId(string Email) 
        {
            var data=db.Users.Where(x => x.Email == Email).SingleOrDefault();
            if(data != null)
            {
                return Json($"Email {Email} Aleardy in Used");
            }
            else
            {
                return Json(true);
            }
        
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User u) 
        {
            u.Role = "User";
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }

        public IActionResult AllUser()
        {
            var data = db.Users.ToList();
            return Json(data);
        }
    }
}
