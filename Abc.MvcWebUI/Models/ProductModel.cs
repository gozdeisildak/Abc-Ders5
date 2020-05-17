using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Class { get; set; }
        public double DaysPrice { get; set; }
        public int LuggageVolume { get; set; }
        public string ModelYears { get; set; }
        public string Types { get; set; }
        public int HowmanyPeople { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }

        public int HorsePower { get; set; }

        public string Size { get; set; }
        public int CategoryId { get; set; }
    }
}