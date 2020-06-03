using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class ViewProduct
    {
        public int Id { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
       
    }
}