using EcommerceUsingDotNetCore.Context;
using EcommerceUsingDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddProduct()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel pm)
        {

            var path = env.WebRootPath;
            var filepath = "/Content/Images/" + pm.Image.FileName;
            var fullPath = Path.Combine(path + filepath);
            UploadFile(pm.Image, fullPath);
            var obj = new Product()
            {
                Name = pm.PName,
                Category = pm.Category,
                Description = pm.Description,
                Price = pm.Price,
                Image = filepath
            };
            db.Add(obj);
            db.SaveChanges();
            TempData["msg"] = "Product Added SucessFully !!";
            return RedirectToAction("Index");
        }

        private void UploadFile(IFormFile file, string fullPath)
        {
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
