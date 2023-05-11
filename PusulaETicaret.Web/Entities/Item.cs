using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Entities
{
    public class Item

    {
        [Key]
        public int ItemId { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
