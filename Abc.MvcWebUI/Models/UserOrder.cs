using Abc.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class UserOrder
    {
        public int Id { get; set; }
        public string TransactionName { get; set; }
        public string OrderNumber { get; set; }
        public string DeliverName { get; set; }
        public double Total { get; set; }
        public EnumOrderState OrderState { get; set; }
        public DateTime OrderDate { get; set; }
        public string PickCity { get; set; }
        public string LeaveCity { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime LeaveTime { get; set; }
    }
}