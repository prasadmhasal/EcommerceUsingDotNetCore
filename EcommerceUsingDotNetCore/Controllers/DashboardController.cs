using EcommerceUsingDotNetCore.Context;
using EcommerceUsingDotNetCore.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db;
        public DashboardController(ApplicationDbContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            string user = HttpContext.Session.GetString("Email");
            if (user != null)
            {
            var data =  HttpContext.Session.GetString("Email");
            TempData["Session"] = data;
            return View();
            }
            return RedirectToAction("Index", "Auth");
        }

        public IActionResult About() 
        {
            return View();
        }

        public IActionResult ViewCart(cart c) 
        {
            string user = HttpContext.Session.GetString("Email");
            var data = db.Carts.Where(x => x.Suser == user);
            if (data != null)
            {
               var  view = db.Carts.ToList();
                var totalprice = 0.0;
                var data2 = db.Carts.Where(x => x.Suser == user);
                foreach (var a in data2)
                {
                    totalprice = totalprice + a.Price;
                }
                TempData["TotalPrice"] = totalprice;
                return View(view);
            }
            else
            {
                TempData["null"] = "No Product You have Added";
            }
            return View();
            
        }


       

    }
}
