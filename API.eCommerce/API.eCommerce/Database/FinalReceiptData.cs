using API.eCommerce.EC;
using API.eCommerce.Models;
using Newtonsoft.Json;

namespace API.eCommerce.Database
{
    public static class FinalReceiptData
    {
        private static Receipt receipt;

        public static Receipt Receipt
        {
            get { return receipt; }

        }
    

    public static void SetReceipt(Receipt newReceipt)
        {
            receipt = newReceipt;
        }
    }
}

