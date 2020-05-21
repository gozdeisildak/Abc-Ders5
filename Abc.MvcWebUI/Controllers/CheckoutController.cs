using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class CheckoutController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Reservation(int id)
        {
            TempData["id"] = id;
            RemoveFromCart();
            AddToCart(id);          
            return View();
        }
        [HttpPost]
        private void AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

           
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

        public ActionResult doReservation(string DeliverName,string TcNo,string city1, string city2, string tarih1, string tarih2)
        {
            var entitiy = new RentDetails();
            entitiy.DeliverName = DeliverName;
            entitiy.TcNo=TcNo;
            entitiy.PickCity = city1;
            entitiy.LeaveCity = city2;
            entitiy.PickUpTime = Convert.ToDateTime(tarih1);
            entitiy.LeaveTime = Convert.ToDateTime(tarih2);
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoCar", "There is no Car in your cart");
            }
            else
            {
               
                    //SİPARİŞİN VERİTABANINA KAYIT EDİLİDĞİ YERR!
                   SaveReservation(cart,entitiy);
                    cart.Clear();
                    return View("doReservation");
               
            }
            return View(new RentDetails());           
        }
        [Authorize]
        public ActionResult GotoPayment(int id)
        {
            RemoveFromCart();
            AddToCart(id);
            return View();
        }
        [HttpPost]
        public ActionResult GotoPayment(string DeliverName, string TcNo, string city1, string city2, string tarih1, string tarih2)
        {
            var entitiy = new RentDetails();
            entitiy.DeliverName = DeliverName;
            entitiy.TcNo = TcNo;
            entitiy.PickCity = city1;
            entitiy.LeaveCity = city2;
            entitiy.PickUpTime = Convert.ToDateTime(tarih1);
            entitiy.LeaveTime = Convert.ToDateTime(tarih2);
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoCar", "There is no Car in your cart");
            }
            else
            {

                //SİPARİŞİN VERİTABANINA KAYIT EDİLİDĞİ YERR!
                SaveRent(cart, entitiy);
                cart.Clear();
                return View("doReservation");

            }
            return View(new RentDetails());
            
        }

        public void RemoveFromCart()
        {           
                GetCart().Clear();             
        }
        public void SaveReservation(Cart cart,RentDetails entitiy)
        {
            var reservation = new Reservation();        

            reservation.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            reservation.Total = cart.Total();
            reservation.OrderDate = DateTime.Now;
            reservation.PickCity = entitiy.PickCity;
            reservation.LeaveCity = entitiy.LeaveCity;
            reservation.PickUpTime = entitiy.PickUpTime;
            reservation.LeaveTime = entitiy.LeaveTime;
            reservation.Tcno = entitiy.TcNo;
            reservation.DeliverName = entitiy.DeliverName;

            reservation.ReservationLines = new List<ReservationLine>();
            foreach (var item in cart.CartLines)
            {
                var reservationline = new ReservationLine();
                reservationline.Price = item.Product.DaysPrice;
                reservationline.Quantity = 1;
                reservationline.ProductId = item.Product.Id;
                reservation.ReservationLines.Add(reservationline);
            }
            db.Reservations.Add(reservation);
            db.SaveChanges();
        }
        public void SaveRent(Cart cart, RentDetails entitiy)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.PickCity = entitiy.PickCity;
            order.LeaveCity = entitiy.LeaveCity;
            order.PickUpTime = entitiy.PickUpTime;
            order.LeaveTime = entitiy.LeaveTime;
            order.Tcno = entitiy.TcNo;
            order.DeliverName = entitiy.DeliverName;

            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                var orderLine = new OrderLine();
                orderLine.Price = item.Product.DaysPrice;
                orderLine.Quantity = 1;
                orderLine.ProductId = item.Product.Id;
                order.OrderLines.Add(orderLine);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }

    }
}