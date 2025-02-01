using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product?>();
        }

        private int LastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Product?> Products { get; private set; }


        public Product AddorUpdate(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }
            return product;
        }

        public Product? Delete(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            Product? product = Products.FirstOrDefault(p => p.Id == Id);
            Products.Remove(product);

            return product;
        }

        public void ChangeItemQuantity(int id, int amount)
        {
            Product? product = Products.FirstOrDefault(p => p?.Id == id);
            if (product != null)
            {
                product.Quantity -= amount;
                if (product.Quantity == 0)
                {
                    Products.Remove(product);
                }
            }
        }

        //Two functions that both are for the cart's update but behave differently depending on if you're adding or subtracting from the cart
        public void AddUpdateCart(int id, int amount, Product product)
        {
            Product? prod = Products.FirstOrDefault(p => p?.Id == id);
            if (prod == null)
            {
                Products.Add(new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = amount
                });
            }
            else
            {
                prod.Quantity += amount;
            }

        }

        public bool RemoveUpdateCart(int id, int amount)
        {
            Product? prod = Products.FirstOrDefault(p => p?.Id == id);

            if (amount < prod.Quantity)
            {
                prod.Quantity -= amount;
                return true;
            }
            else if (amount == prod.Quantity)
            {
                Current.Delete(prod.Id);
                return true;
            }
            else return false;
        }
    }
}