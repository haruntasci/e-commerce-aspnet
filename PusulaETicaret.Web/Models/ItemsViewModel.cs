namespace PusulaETicaret.Web.Models
{
    public class ItemsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Total
        {
            get { return Quantity * ProductPrice; }
        }
        public int Quantity { get; set; }
        public string Image { get; set; }

    }
}
