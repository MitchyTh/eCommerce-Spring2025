using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class ReceiptServiceProxy
    {
        public static ReceiptServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReceiptServiceProxy();
                }

                return instance;
            }
        }
        private static ReceiptServiceProxy? instance;

        public ReceiptServiceProxy() 
        {
            var receiptPayload = new WebRequestHandler().Get("/Receipt").Result;

            var receipt = JsonConvert.DeserializeObject<ReceiptDataContainer>(receiptPayload);

            Data = new ReceiptDataContainer()
            {
                Items = receipt.Items,
                Subtotal = receipt.Subtotal,
                Tax = receipt.Tax,
                Total = receipt.Total,
            };
        }

        public ReceiptDataContainer Data {  get; set; }

    }
}
