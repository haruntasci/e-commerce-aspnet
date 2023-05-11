using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Image { get; set; }   
    }
}
