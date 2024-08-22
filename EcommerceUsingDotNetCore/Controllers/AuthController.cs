using EcommerceUsingDotNetCore.Context;
using EcommerceUsingDotNetCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Text;

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

        public static string  DecrptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] pass = Convert.FromBase64String(password);
                string decrpass = ASCIIEncoding.ASCII.GetString(pass);
                return decrpass;
            }
        }

        [HttpPost]
        public  IActionResult SignIn(LoginModel log)
        {
            var data = db.Users.Where(x => x.Email.Equals(log.Email)).SingleOrDefault();
            if (data != null)
            {
                bool us = data.Email.Equals(log.Email) && DecrptPassword(data.Password).Equals(log.Password);
                if (us)
                {
                    HttpContext.Session.SetString("Email",data.Email);
                    TempData["login"] = "Successfully Login !!";
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["Error"] = "Incorrect Password";
                }
            }
            else
            {
                TempData["ErrorEmail"] = "Incorrect Email";
            }
            return View();

        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
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

        public static string  EncryptedPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] pass = ASCIIEncoding.ASCII.GetBytes(password);
                string encrpass = Convert.ToBase64String(pass);
                return encrpass;
            }
        }
        [HttpPost]
        public IActionResult SignUp(User u) 
        {
            u.Role = "User";
            u.Password = EncryptedPassword(u.Password);
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
