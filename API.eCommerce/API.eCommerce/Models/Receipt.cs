using Library.eCommerce.Models;

namespace API.eCommerce.Models
{
    public class Receipt
    {
        public List<Item> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
