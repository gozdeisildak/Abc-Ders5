using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    [Authorize(Roles ="admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i=> new UserOrder()
            {

                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                PickCity = i.PickCity,
                LeaveCity = i.LeaveCity,
                PickUpTime = i.PickUpTime,
                LeaveTime = i.LeaveTime,
                TransactionName = "Rent"
            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }
        public ActionResult Reserv()
        {
            var reserv = db.Reservations.Select(i => new UserOrder() {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                PickCity = i.PickCity,
                LeaveCity = i.LeaveCity,
                PickUpTime = i.PickUpTime,
                LeaveTime = i.LeaveTime,
                TransactionName = "Reservation"
            }).OrderByDescending(i=>i.OrderDate).ToList();
            return View(reserv);
        }
    }
}