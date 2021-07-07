using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel;
>>>>>>> AdminChanges
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class RentDetails
    {
<<<<<<< HEAD
        public string DeliverName { get; set; }
        public string TcNo { get; set; }

        [Required(ErrorMessage ="Please Enter Adress that you leave car")]
        public string LeaveCity { get; set; }  //bırakılacak yer

        [Required(ErrorMessage ="Please enter a City")]
        public string  PickCity { get; set; }

        public DateTime PickUpTime { get; set; }
=======
        [Required(ErrorMessage = "Please enter a Deliver Name")]
        [DisplayName("The name of the person who will receive the car")]
        [DefaultValueAttribute("SomethingDefault")]
        public string DeliverName { get; set; }

        [Required(ErrorMessage = "Please enter a TC Number")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Please enter a valid TC Number")]
        [DisplayName("Identity Number of the person who will receive the car")]
        [DefaultValue("12125369874")]
        public string TcNo { get; set; }

        [Required(ErrorMessage ="Please Enter Adress that you leave car")]
        [DisplayName("Leave location")]
        public string LeaveCity { get; set; }  //bırakılacak yer

        [Required(ErrorMessage = "Please Enter Adress that you pick up car")]
        [DisplayName("Pick-up location")]
        public string  PickCity { get; set; }

        [Editable(true)]
        [Required(ErrorMessage = "Please enter a pick up time")]
        [DisplayName("Pick-up time")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PickUpTime { get; set; }

        [Required(ErrorMessage = "Please enter a leave time")]
        [DisplayName("Leaving time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

>>>>>>> AdminChanges
        public DateTime LeaveTime { get; set; }
        
    }
}