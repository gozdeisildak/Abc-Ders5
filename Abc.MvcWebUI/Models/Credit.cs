﻿using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
>>>>>>> AdminChanges
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class Credit
    {
<<<<<<< HEAD
        public string CartName { get; set; }
        public string CardNo { get; set; }
        public string CardTarihi { get; set; }
=======

        [Required]
        [DisplayName("Card owner's Name")]
        public string CartName { get; set; }
        [Required]
        [DisplayName("Card No")]
        [CreditCard]
        public string CardNo { get; set; }
        [Required]
        [DisplayName("Card Date")]
        public string CardTarihi { get; set; }
        [Required]
        [DisplayName("CV Number")]
>>>>>>> AdminChanges
        public int CVNo { get; set; }
        public int ReservationId { get; set; }
        public int OrderId { get; set; }

    }
}