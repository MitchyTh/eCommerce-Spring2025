
using API.eCommerce.Models;
using Library.eCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Api.eCommerce.Database
{
    public class Filebase
    {
        private string _root;
        private string _productRoot;
        private string _cartRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\\temp";
            _productRoot = $"{_root}\\Products";
            _cartRoot = $"{_root}\\Cart";
        }

        public int LastKey
        {
            get
            {
                if (Inventory.Any())
                {
                    return Inventory.Select(x => x.Id).Max();
                }
                return 0;
            }
            set { }
        }


        public Item AddOrUpdate(string type, Item item)
        {
            string folderPath = type.ToLower() switch
            {
                "inventory" => _productRoot,    // e.g., C:\temp\Products
                "cart" => _cartRoot,       // e.g., C:\temp\Cart
                _ => null
            };



            //set up a new Id if one doesn't already exist
            if (item.Id <= 0)
            {
                item.Id = LastKey + 1;
                item.Product.Id = LastKey;
            }

            string path = $"{folderPath}\\{item.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id

            return item;
        }

        public List<Item?> Inventory
        {
            get
            {
                var root = new DirectoryInfo(_productRoot);
                var products = new List<Item>();

                if (!root.Exists)
                {
                    Directory.CreateDirectory(_productRoot); // Create if it doesn't exist
                }

                foreach (var productFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Item>
                        (File.ReadAllText(productFile.FullName));
                    if (patient != null)
                    {
                        products.Add(patient);
                    }

                }
                return products;
            }
        }

        public List<Item?> Cart
        {
            get
            {
                var root = new DirectoryInfo(_cartRoot);
                var cart = new List<Item?>();

                if (!root.Exists)
                {
                    Directory.CreateDirectory(_cartRoot); // Create if it doesn't exist
                }

                foreach (var productFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Item>
                        (File.ReadAllText(productFile.FullName));
                    if (patient != null)
                    {
                        cart.Add(patient);
                    }

                }
                return cart;
            }
        }

        public bool Delete(string type, int id)
        {
            string folderPath = type.ToLower() switch
            {
                "inventory" => _productRoot,    // e.g., C:\temp\Products
                "cart" => _cartRoot,       // e.g., C:\temp\Cart
                _ => null
            };

            if (folderPath == null) return false;

            string path = $"{folderPath}\\{id}.json";

            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }



        public static IEnumerable<Item> Search(string? query)
        {
            return _instance.Inventory.Where(p => p?.Product?.Name?.ToLower()
                    .Contains(query?.ToLower() ?? string.Empty) ?? false);
        }
    }
}
