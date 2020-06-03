
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> Orderlines { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationLine> ReservationLines { get; set; }
        public DbSet<CreditCart> CreditCarts { get; set; }
        public DbSet<ViewProduct> View_Products { get; set; }
    }
}