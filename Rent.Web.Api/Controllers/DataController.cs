using Abc.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace Rent.Web.Api.Controllers
{
    public class DataController : ApiController
    {
        public List<Order> GetProducts()
        {
            var db = new DataContext();
            var data = db.Orders.Where(x => x.PickCity == "Bursa").ToList();
            return data;
        }
           

    }
}
