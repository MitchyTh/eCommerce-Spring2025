using Api.eCommerce.Database;
using API.eCommerce.Controllers;
using API.eCommerce.Database;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Razor.Hosting;

namespace API.eCommerce.EC
{
    public class CartEC
    {
        public List<Item?> Get()
        {
            return Filebase.Current.Cart;
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingItem = Filebase.Current.Cart.FirstOrDefault(p => p?.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                Filebase.Current.AddOrUpdate("cart", existingItem);
            }
            else
            {
                item.Quantity = 1;
                Filebase.Current.AddOrUpdate("cart", item);
            }

            var inventoryItem = Filebase.Current.Inventory.FirstOrDefault(p => p?.Id == item.Id);
            if (inventoryItem != null)
            {
                --inventoryItem.Quantity;
                Filebase.Current.AddOrUpdate("inventory", inventoryItem);

                if (inventoryItem.Quantity == 0)
                {
                    Filebase.Current.Delete("inventory", inventoryItem.Id);
                }
            }
            return item;
        }
        public Item? ReturnItem(Item item)
        {
            var existingItem = Filebase.Current.Cart.FirstOrDefault(p => p?.Id == item.Id);
            if (existingItem != null)
            {
                --existingItem.Quantity;
                Filebase.Current.AddOrUpdate("cart", existingItem); // Save updated quantity
                if (existingItem.Quantity == 0)
                {
                    Filebase.Current.Delete("cart", item.Id);
                }
            }
            else
            {
                return item;
            }

            var inventoryItem = Filebase.Current.Inventory.FirstOrDefault(p => p?.Id == item.Id);
            if (inventoryItem == null)
            {
                item.Quantity = 1;
                Filebase.Current.AddOrUpdate("inventory", item);
            }
            else
            {
                inventoryItem.Quantity++;
                Filebase.Current.AddOrUpdate("inventory", inventoryItem); // Update existing inventory
            }
            return item;
        }
    }
}
