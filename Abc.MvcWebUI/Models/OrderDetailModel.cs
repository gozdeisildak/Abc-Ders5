using Abc.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class OrderDetailModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliverName { get; set; }
        public string Tcno { get; set; }
        public string PickCity { get; set; } 
        public string LeaveCity { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public EnumOrderState OrderState { get; set; }
        public virtual List<OrderLineDetailModel> OrderLineDetails { get; set; }
       
    }
    public class OrderLineDetailModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }
}