using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class RentDetails
    {
        public string DeliverName { get; set; }
        public string TcNo { get; set; }

        [Required(ErrorMessage ="Please Enter Adress that you leave car")]
        public string LeaveCity { get; set; }  //bırakılacak yer

        [Required(ErrorMessage ="Please enter a City")]
        public string  PickCity { get; set; }

        public DateTime PickUpTime { get; set; }
        public DateTime LeaveTime { get; set; }
        
    }
}