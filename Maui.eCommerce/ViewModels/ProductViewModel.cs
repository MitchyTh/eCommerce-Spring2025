using Library.eCommerce.Services;
using Library.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Maui.eCommerce.ViewModels
{
    class ProductViewModel
    {
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        private Item? cachedModel {  get; set; }
        public string? Name
        {
            get
            {
                return Model?.Product?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model.Product?.Name != value)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Model?.Quantity;
            }
            set
            {
                if(Model != null && value != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public decimal? Price
        {
            get
            {
                return Model?.Product.Price;
            }
            set
            {
                if (Model != null && value != null && Model.Product.Price != value)
                {
                    Model.Product.Price = value;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddorUpdate(Model);
        }

        public void Undo()
        {
            ProductServiceProxy.Current.AddorUpdate(cachedModel);
        }
        public ProductViewModel()
        {
            Model = new Item();
            cachedModel = null;
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
            if (model != null)
            {
                cachedModel = new Item(model);
            }
        }
    }
}
