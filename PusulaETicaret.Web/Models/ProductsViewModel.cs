using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Models
{
    public class ProductsViewModel
    {
      
            public int ProductId { get; set; }

            [Required(ErrorMessage = "Ürün adı giriniz.")]
            public string ProductName { get; set; }

            [Required(ErrorMessage = "Ürün fiyatı giriniz.")]
             public decimal ProductPrice { get; set; }           
             public string Image { get; set; }

            [Required(ErrorMessage = "Resim ekleyiniz.")]
             public IFormFile File { get; set; }


    }
}
