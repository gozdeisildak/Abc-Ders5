using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
      
        public ActionResult doReservation(RentDetails rentCar)
        {
            TempData.Keep();
            //string DeliverName, string TcNo, string city1, string city2, string tarih1, string tarih2

            //var entitiy = new RentDetails();
            //entitiy.DeliverName = DeliverName;
            //entitiy.TcNo = TcNo;
            //entitiy.PickCity = city1;
            //entitiy.LeaveCity = city2;
            //entitiy.PickUpTime = Convert.ToDateTime(tarih1);
            // entitiy.LeaveTime = Convert.ToDateTime(tarih2);
            rentCar.PickUpTime = Convert.ToDateTime(TempData.Peek("tarih1"));
            rentCar.LeaveTime = Convert.ToDateTime(TempData.Peek("tarih2"));
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoCar", "There is no Car in your cart");
            }
            else
            {
                //SİPARİŞİN VERİTABANINA KAYIT EDİLİDĞİ YERR!
                SaveReservation(cart, rentCar);
                cart.Clear();
               
            }
            return View("doReservation");
        }
        [Authorize]
        public ActionResult GotoPayment(int id)
        {
            RemoveFromCart();
            AddToCart(id);
            return View();
        }
        [HttpPost]
        public ActionResult GotoPayment(RentDetails rentCar)
        {
            rentCar.PickUpTime = Convert.ToDateTime(TempData.Peek("tarih1"));
            rentCar.LeaveTime = Convert.ToDateTime(TempData.Peek("tarih2"));
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoCar", "There is no Car in your cart");
            }
            else
            {

                //SİPARİŞİN VERİTABANINA KAYIT EDİLİDĞİ YERR!
                SaveRent(cart, rentCar);
                cart.Clear();
                return RedirectToAction("PayRent");

            }
            return View(new RentDetails());

        }

        public void RemoveFromCart()
        {
            GetCart().Clear();
        }
        public void SaveReservation(Cart cart, RentDetails entitiy)
        {
            var reservation = new Reservation();

            reservation.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            TimeSpan kalangun = entitiy.PickUpTime - entitiy.LeaveTime;//Sonucu zaman olarak döndürür
            int toplamGun =Convert.ToInt32( kalangun.TotalDays);
            reservation.Total = (cart.Total()/2)*toplamGun;
            reservation.OrderDate = DateTime.Now;
            reservation.PickCity = entitiy.PickCity;
            reservation.LeaveCity = entitiy.LeaveCity;
            reservation.PickUpTime = entitiy.PickUpTime;
            reservation.LeaveTime = entitiy.LeaveTime;
            reservation.Tcno = entitiy.TcNo;
            reservation.DeliverName = entitiy.DeliverName;
            reservation.OrderState = EnumOrderState.Waiting;

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
            var user = User.Identity.GetUserId();
            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            TimeSpan kalangun = entitiy.PickUpTime - entitiy.LeaveTime;//Sonucu zaman olarak döndürür
            int toplamGun = Convert.ToInt32(kalangun.TotalDays);
            order.Total = cart.Total()*toplamGun;
            order.OrderDate = DateTime.Now;
            order.PickCity = entitiy.PickCity;
            order.LeaveCity = entitiy.LeaveCity;
            order.PickUpTime = entitiy.PickUpTime;
            order.LeaveTime = entitiy.LeaveTime;
            order.Tcno = entitiy.TcNo;
            order.DeliverName = entitiy.DeliverName;
            order.OrderState = EnumOrderState.Waiting;

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

        public ActionResult PayRent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PayRent(String CardName, String CardNo, String CardTarihi, String CVNo)
        {         
                var credit = new CreditCart();
                var orderid = db.Orders.Max(p => p.Id);
                credit.CardNo = CardNo;
                credit.CardTarihi = CardTarihi;
                credit.CartName = CardName;
                credit.CVNo = Convert.ToInt32(CVNo);
                credit.OrderId = orderid;
                credit.ProductId = db.Orders.OrderByDescending(p => p.Id).FirstOrDefault().OrderLines.Select(p => p.ProductId).FirstOrDefault();
                db.CreditCarts.Add(credit);
                db.SaveChanges();
                //db.Orders.Where(p=>p.Id==orderid).;
                return View("Index");                    

        }
       
      
        public ActionResult PayReserve(String CardName, String CardNo, String CardTarihi,String CVNo)
        {
            TempData.Keep();
                          
                var credit = new CreditCart();
                var reservationid = db.Reservations.Max(p => p.Id);
                credit.CardNo = CardNo;
                credit.CardTarihi =CardTarihi;
                credit.CartName = CardName;
                credit.CVNo = Convert.ToInt32(CVNo);
                credit.OrderId= reservationid;
                credit.ProductId = db.Reservations.OrderByDescending(p => p.Id).FirstOrDefault().ReservationLines.Select(p=>p.ProductId).FirstOrDefault(); 
                db.CreditCarts.Add(credit);
                db.SaveChanges();
            return View("Index");           
            
        }

        public ActionResult DeleteReservation(int id)
        {
            var ids = db.ReservationLines.OrderByDescending(p => p.Id).FirstOrDefault();
            var rid = new Reservation();
            rid = db.Reservations.OrderByDescending(p => p.Id).FirstOrDefault();
            db.Reservations.Remove(rid);
            db.ReservationLines.Remove(ids);           
            return RedirectToAction("Reservation",id);
        }
        public ActionResult DeletePayment(int id)
        {
            var ids = db.Orderlines.OrderByDescending(p => p.Id).FirstOrDefault();
            var rid = new Order();
            rid = db.Orders.OrderByDescending(p => p.Id).FirstOrDefault();
            db.Orders.Remove(rid);
            db.Orderlines.Remove(ids);
            return RedirectToAction("GoToPayment", id);
        }

    }
}