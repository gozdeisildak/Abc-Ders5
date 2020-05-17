using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Car Name")]
        public string Model { get; set; }
        [DisplayName("Car Descripiton")]
        public string Class { get; set; }
        public double DaysPrice { get; set; }
        public int LuggageVolume { get; set; }
        public int ModelYears { get; set;}
        public int HorsePower { get; set; }
        public string Types { get; set; }
        public int HowmanyPeople { get; set; }
        public string Image { get; set; }
        public bool HaveKlima { get; set; }
        public bool IsHome { get; set; }//anadayfa için
        public bool IsApproved { get; set; }///admin onaylamış mı
        public string Size { get; set; }
        public string Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}