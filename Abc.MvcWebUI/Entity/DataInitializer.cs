using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
                new Category(){ Name = "İstanbul", Description = "Airport"},
                new Category(){ Name = "Ankara", Description = "Mamak"},
                new Category(){ Name = "İzmir", Description = "Airport"},
                new Category(){ Name = "Bursa", Description = "Nilüfer"},
                new Category(){ Name = "Muğla", Description = "Fethiye"}
            };

            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();

            var urunler = new List<Product>()
            {
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi B", DaysPrice =1200 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 1,IsHome = false,Image = "2.png",Brand="MERCEDES"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi A", DaysPrice =1500 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 2,IsHome = true,Image = "2.png",Brand="BMW"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi C", DaysPrice =1800 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 3,IsHome = true,Image = "3.png",Brand="FIAT"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi B", DaysPrice =1200 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 4,IsHome = false,Image = "4.png",Brand="FORD"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi A", DaysPrice =1100 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 5,IsHome = true,Image = "2.png",Brand="MERCEDES"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi B", DaysPrice =1200 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 1,IsHome = false,Image = "2.png",Brand="FIAT"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi C", DaysPrice =1400 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 2,IsHome = true,Image = "3.png",Brand="MERCEDES"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi B", DaysPrice =1200 , LuggageVolume =500 ,ModelYears=2000,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 1,IsHome = false,Image = "2.png",Brand="bmw"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi A", DaysPrice =1500 , LuggageVolume =500 ,ModelYears=2005,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 2,IsHome = true,Image = "4.png",Brand="BMW"},
                new Product(){ Model = "BMW SERIES 1 1.5 116D AUTO 2017",Class = "Economi B", DaysPrice =1200 , LuggageVolume =500 ,ModelYears=2020,HorsePower=116,Types="Hatchback",HowmanyPeople=5, IsApproved =true,HaveKlima=true , CategoryId = 1,IsHome = true,Image = "2.png",Brand="FORD"},

            };

            foreach (var urun in urunler)
            {
                context.Products.Add(urun);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
