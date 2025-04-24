using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ShoppingCartService()
        {
            var cartPayload = new WebRequestHandler().Get("/Cart").Result;
            Items = JsonConvert.DeserializeObject<List<Item?>>(cartPayload) ?? new List<Item?>();
        }
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        public List<Item> Items;

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

        public Item? AddOrUpdate(Item item)
        {
            //CALL THE WEB SERVICE
            var response = new WebRequestHandler().Post("/Cart/add", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);
            if (newItem == null)
            {
                return item;
            }

            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }

            var existingCartItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingCartItem == null)
            {
                var newCartItem = new Item(item);
                newItem.Quantity = 1;
                Items.Add(new Item(newItem));
            }
            else
            {
                existingCartItem.Quantity++;
            }

            return existingInvItem;
        }

        public Item? ReturnItem(Item item)
        {
            //CALL WEB SERVICE
            var response = new WebRequestHandler().Post("/Cart/return", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);
            if (newItem == null)
            {
                return item;
            }

            if (item.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = Items.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }

            }

            return itemToReturn;
        }

        //public Receipt? GetFinalBill()
        //{
        //    var response = new WebRequestHandler().Post("/Receipt/purchase", null).Result;
        //    return JsonConvert.DeserializeObject<Receipt>(response);
        //    //decimal? finalBill = 0;
        //    //foreach (var item in Items)
        //    //{
        //    //    finalBill += item.TotalPrice;
        //    //}
        //    //return finalBill;

       
    }
}
