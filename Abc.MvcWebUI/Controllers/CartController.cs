using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product,1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        [Authorize]
        public ActionResult Checkout()
        {
            return View(new RentDetails());
        }
        [HttpPost]
        public ActionResult Checkout(RentDetails entity)
        {
            var cart = GetCart();
            if (cart.CartLines.Count==0)
            {
                ModelState.AddModelError("NoCar","There is no Car in your cart");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //SİPARİŞİN VERİTABANINA KAYIT EDİLİDĞİ YERR!
                   // SaveOrder(cart,entity);
                    cart.Clear();
                    return View("Completed");
                }
                else
                {
                    return View();
                }
            }
            return View(new RentDetails());
        }
    }
    //private void SaveOrder(Cart cart, RentDetails entity)
    //{
    //    var order = new Order();
    //    order.OrderNumber = "A"+(new Random()).Next(111111,999999).ToString();
    //    order.Total = cart.Total();
    //    order.OrderDate = DateTime.Now;
        
    //}
}