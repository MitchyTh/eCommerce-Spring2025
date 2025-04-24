using Library.eCommerce.Models;

namespace API.eCommerce.Database
{
    public static class ShoppingCartDatabase
    {
        private static List<Item?> cart = new List<Item?>();

        public static List<Item?> Cart { get { return cart; } }
    }
}
