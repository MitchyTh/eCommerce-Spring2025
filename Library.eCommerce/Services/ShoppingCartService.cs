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
        private ProductServiceProxy _prodsvc;
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
    }
}
