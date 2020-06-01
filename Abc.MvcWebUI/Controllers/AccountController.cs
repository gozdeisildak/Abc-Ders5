using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Identity;
using Abc.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Abc.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        private DataContext db = new DataContext();
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }
        public ActionResult Index()  //// Sayfada kullanıcının 
        {
            var username = User.Identity.Name;
            var reservation = db.Reservations.Where(i => i.DeliverName == username).Select(i => new UserOrder()
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
                TransactionName = "Reservation"
            }).OrderByDescending(i=>i.OrderDate).ToList();
            var order= db.Orders.Where(i => i.DeliverName == username).Select(i => new UserOrder()
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
            foreach (var item in order)
            {
                reservation.Add(item);
            }
            return View(reservation);
        }
        public ActionResult Details(int id,string transaction)
        {
            if (transaction == "Rent")
            {
                var entity = db.Orders.Where(i => i.Id == id)
                               .Select(i => new OrderDetailModel()
                               {
                                   OrderId = i.Id,
                                   OrderNumber = i.OrderNumber,
                                   Total = i.Total,
                                   OrderDate = i.OrderDate,
                                   OrderState = i.OrderState,
                                   PickCity = i.PickCity,
                                   LeaveCity = i.LeaveCity,
                                   PickUpTime = i.PickUpTime,
                                   LeaveTime = i.LeaveTime,
                                   DeliverName = i.DeliverName,
                                   Tcno = i.Tcno,
                                   OrderLineDetails = i.OrderLines.Select(a => new OrderLineDetailModel()
                                   {
                                       ProductId = a.ProductId,
                                       ProductName = a.Product.Model,
                                       Image = a.Product.Image,
                                       Price = a.Price
                                   }).ToList()
                               }).FirstOrDefault();
                return View(entity);
            }
            else if(transaction == "Reservation")
            {
                var entity = db.Reservations.Where(i => i.Id == id)
                               .Select(i => new OrderDetailModel()
                               {
                                   OrderId = i.Id,
                                   OrderNumber = i.OrderNumber,
                                   Total = i.Total,
                                   OrderDate = i.OrderDate,
                                   OrderState = i.OrderState,
                                   PickCity = i.PickCity,
                                   LeaveCity = i.LeaveCity,
                                   PickUpTime = i.PickUpTime,
                                   LeaveTime = i.LeaveTime,
                                   DeliverName = i.DeliverName,
                                   Tcno = i.Tcno,
                                   OrderLineDetails = i.ReservationLines.Select(a => new OrderLineDetailModel()
                                   {
                                       ProductId = a.ProductId,
                                       ProductName = a.Product.Model,
                                       Image = a.Product.Image,
                                       Price = a.Price
                                   }).ToList()
                               }).FirstOrDefault();
                return View(entity);
            }
            return View();
        }

        // GET: Account
        public ActionResult Register()

        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                //Kayıt işlemleri

                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    //kullanıcı oluştu ve kullanıcıyı bir role atayabilirsiniz.
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                   
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı  oluşturma hatası.");
                }

            }

            return View(model);
        }

        // GET: Account
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Home/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model,string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Login işlemleri
                    var user = await UserManager.FindAsync(model.UserName, model.Password);
                    var role = RoleManager.FindByName("admin");
                    if (user != null)
                    {
                        // varolan kullanıcıyı sisteme dahil et.
                        // ApplicationCookie oluşturup sisteme bırak.

                        var authManager = HttpContext.GetOwinContext().Authentication;
                        var identityclaims =await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        var authProperties = new AuthenticationProperties();
                        authProperties.IsPersistent = false;
                        authManager.SignIn(authProperties, identityclaims);

                       
                        if (user.Roles.Select(i => i.RoleId).First() == role.Id)
                        {
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");
            }
           

            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index","Home");
        }

    }
}