using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.eCommerce.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public string? Display
        {
            get
            {
                return $"{Id}. {Name} ${Price} Amount: {Quantity}";
            }
        }

        public Product()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}