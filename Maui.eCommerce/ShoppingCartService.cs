using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodsvc = ProductServiceProxy.Current;
        private List<Item> items;
        public List<Item> cartItems
        {
            get
            {
                return items;
            }
        }
        public static ShoppingCartService Current {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            }
        }
        private static ShoppingCartService? instance;
        private ShoppingCartService() {
            items = new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodsvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }

            var existingItem = cartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                cartItems.Add(new Item(newItem));
            }
            else
            {
                existingItem.Quantity++;
            }

            return existingInvItem;
        }
    }
}
