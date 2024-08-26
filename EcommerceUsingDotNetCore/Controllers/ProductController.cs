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
            var data = db.Product.Take(5).ToList();
            return View(data);
          

        }
        [HttpPost]
        public IActionResult Index(string choice)
        {
            if (choice == "Low To High")
            {
                var data = db.Product.OrderBy(x => x.Price).ToList();
                return View(data);
            }
            else if (choice == "Hign To Low") 
            {
                var data = db.Product.OrderBy(x => x.Price).ToList();
                return View(data);
            }
            else
            { 
            var data = db.Product.Take(5).ToList();
            return View(data);
            }
          
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
            db.Product.Add(obj);
            db.SaveChanges();
            TempData["msg"] = "Product Added SucessFully !!";
            return RedirectToAction("Index");
        }

        private void UploadFile(IFormFile file, string fullPath)
        {
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult AddToCart(int Id)
        {

            string sess = HttpContext.Session.GetString("Email");
            if(Id != 0)
            {

            var pro = db.Product.Find(Id);
            var obj = new Cart()
            {
                Name = pro.Name,
                Category = pro.Category,
                Description = pro.Description,
                Price = pro.Price,
                Image = pro.Image,
                Suser = sess
            };
            db.Carts.Add(obj);
            db.SaveChanges();
            TempData["Add"] = "Successfully Added !!";
            }

            return RedirectToAction("viewCart","Dashboard");



        }
        public IActionResult DeleteToCart(int id)
        { 
            var data = db.Carts.Find(id);
            db.Carts.Remove(data);
            db.SaveChanges();
            return RedirectToAction("AddToCart");

        }


        public ActionResult Payment()
        {
            return View();


        }


    }
}
