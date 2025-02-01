using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class Cart
    {
        public List<Product?> Items { get; set; } = new List<Product?>();

        public void Add(Product product)
        {
            Items.Add(product);
        }

        public Product Update(Product product)
        {
            return product;
        }


        public Product? Delete(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            Product? product = Items.FirstOrDefault(p => p.Id == Id);
            ProductServiceProxy.Current.ChangeItemQuantity(product.Id, -product.Quantity);
            ProductServiceProxy.Current.AddorUpdate(product);
            Items.Remove(product);

            return product;
        }
    }
}