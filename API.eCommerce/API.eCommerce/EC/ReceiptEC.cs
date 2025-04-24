using API.eCommerce.Models;
using Api.eCommerce.Database;
using API.eCommerce.Database;
using Newtonsoft.Json;
namespace API.eCommerce.EC
{
    public class ReceiptEC
    {
        public Receipt Get()
        {
            return FinalReceiptData.Receipt;
        }
        public Receipt FinalizePurchase()
        {
            var cartItems = Filebase.Current.Cart;
            decimal subtotal = (decimal)cartItems.Sum(item => item.TotalPrice);
            decimal tax = subtotal * 0.07m;
            decimal total = subtotal + tax;

            var receipt = new Receipt
            {
                Items = cartItems,
                Subtotal = subtotal,
                Tax = tax,
                Total = total,
            };

            FinalReceiptData.SetReceipt(receipt);
            return receipt;
        }
    }
}
