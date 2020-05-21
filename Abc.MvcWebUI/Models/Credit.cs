using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class Credit
    {
        public string CartName { get; set; }
        public string CardNo { get; set; }
        public string CardTarihi { get; set; }
        public int CVNo { get; set; }
        public int ReservationId { get; set; }
        public int OrderId { get; set; }

    }
}