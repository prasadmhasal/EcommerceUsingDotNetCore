using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class DashboardController : Controller
    {
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

       

    }
}
