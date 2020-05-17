using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();



        // GET: Home
        public ActionResult Index()
        {
         
            return View();

        }

      
        public ActionResult _ProductList(string radioV, string priceR, string favoriteV)
        {
            var query = TempData.Peek("query") as IQueryable<Product>;

            string[] brands = favoriteV.Split(',');
            priceR = priceR.Replace("$", string.Empty);
            priceR = priceR.Replace("-", string.Empty);
            string[] prices = priceR.Split(' ');
            double price1 = Convert.ToDouble(prices[0]);
            double price2 = Convert.ToDouble(prices[2]);
            IEnumerable<Product> urunler = query.AsEnumerable().Where(i => i.DaysPrice > price1 && i.DaysPrice < price2).AsQueryable();

            if (radioV != "all")
                urunler = urunler.Where(i => i.Size == radioV);
            if (favoriteV.Length != 0)
                urunler = urunler.Where(i => brands.Any(term => i.Brand.Contains(term)));

            var products = urunler.Select(i => new ProductModel()
            {
                Id = i.Id,
                Model = i.Model.Length > 50 ? i.Model.Substring(0, 47) + "..." : i.Model,
                Class = i.Class.Length > 50 ? i.Class.Substring(0, 47) + "..." : i.Class,
                DaysPrice = i.DaysPrice,
                Image = i.Image ?? "1.jpg",
                CategoryId = i.CategoryId,
                Brand = i.Brand,
                Size = i.Size
            }).ToList();

            return PartialView(products);

        }
     
        
      
        public ActionResult List(string city1, string city2,string tarih1, string tarih2) 
        {
            TempData["city1"] = city1;
            TempData["tarih1"] = tarih1;
            TempData["tarih2"] = tarih2;

            var queryContext = _context.Products.Where(i => i.Category.Name == city1).AsQueryable();

            TempData["query"] = queryContext;
            

            var products = queryContext.Select(i => new ProductModel()
            {
                Id = i.Id,
                Model = i.Model.Length > 50 ? i.Model.Substring(0, 47) + "..." : i.Model,
                Class = i.Class.Length > 50 ? i.Class.Substring(0, 47) + "..." : i.Class,
                DaysPrice = i.DaysPrice,
                Image = i.Image ?? "1.jpg",
                CategoryId = i.CategoryId,
                Brand=i.Brand,
                Size=i.Size
            }).ToList();

            return View(products);
        }
        public ActionResult Details(int id)
        {
            return View(_context.Products.Where(i => i.Id == id).FirstOrDefault());
        }

        public PartialViewResult GetCategories()
        {

            return PartialView(_context.Categories.ToList());
        }

    }

}