using Microsoft.EntityFrameworkCore;

namespace PusulaETicaret.Web.Entities
{
    public class SeedData
    {
        public static void SeedDatabase(DatabaseContext context)
        {
            context.Database.Migrate();

            if (!context.Products.Any())
            {
             
                context.Products.AddRange(
                        new Product
                        {
                            ProductName = "Lenovo ThinkPad",
                            ProductPrice = 24899,
                            Image = "lenovo.jpg"
                        },
                        new Product
                        {
                            ProductName = "Apple MacBook Pro",
                            ProductPrice = 36799,
                            Image = "macbook.jpg"
                        },
                        new Product
                        {
                            ProductName = "Dell Gaming",
                            ProductPrice = 28999,
                            Image = "dell.jpg"
                        },
                        new Product
                        {
                            ProductName = "Casper Excalibur",
                            ProductPrice = 27699,
                            Image = "casper.jpg"
                        },
                        new Product
                        {
                            ProductName = "iPhone 11",
                            ProductPrice = 17499,
                            Image = "iphone.jpg"
                        },
                        new Product
                        {
                            ProductName = "Xiaomi Redmi Note 11 Pro",
                            ProductPrice = 9175,
                            Image = "xiomi.jpg"
                        },
                        new Product
                        {
                            ProductName = "Samsung Galaxy A23",
                            ProductPrice = 6799,
                            Image = "samsung.jpg"
                        },
                        new Product
                        {
                            ProductName = "Oppo A55",
                            ProductPrice = 5399,
                            Image = "oppo.jpg"
                        },
                        new Product
                        {
                            ProductName = "Regal HD TV",
                            ProductPrice = 4199,
                            Image = "regal.jpg"
                        },
                        new Product
                        {
                            ProductName = "Vestel Smart LED TV",
                            ProductPrice = 14400,
                            Image = "vestel.jpg"
                        },
                        new Product
                        {
                            ProductName = "LG Smart OLED TV",
                            ProductPrice = 29433,
                            Image = "lg.jpg"
                        },
                        new Product
                        {
                            ProductName = "Onvo Smart LED TV",
                            ProductPrice = 3069,
                            Image = "onvo.jpg"
                        }

                );

                context.SaveChanges();
            }
        }
    }
}
