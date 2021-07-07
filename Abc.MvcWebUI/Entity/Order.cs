using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliverName { get; set; }
        public string Tcno { get; set; }
        public string PickCity { get; set; }  //bırakılacak yer
        //eğer burda adress bilgilerini tutmassak ve her order açıldığında rentdetailstan çağırırsa,
        //güncellendiğinde yanlış bilgilere sahip oluruz...
        public string LeaveCity { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public EnumOrderState OrderState { get; set; }
        public bool Confirm { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId  { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}