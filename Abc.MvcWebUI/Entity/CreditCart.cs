using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class CreditCart
    {
        public int Id { get; set; }
        public string CartName { get; set; }
        public string CardNo { get; set; }
        public string CardTarihi { get; set; }
        public int CVNo { get; set; }



        public int  OrderId { get; set; }
        public int ProductId { get; set; }


    }
}