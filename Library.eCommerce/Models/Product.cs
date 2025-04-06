﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.eCommerce.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name} ${Price}";
            }
        }

        public string LegacyProperty1 { get; set; }
        public string LegacyProperty2 { get; set; }
        public string LegacyProperty3 { get; set; }
        public string LegacyProperty4 { get; set; }
        public string LegacyProperty5 { get; set; }
        public string LegacyProperty6 { get; set; }

        public Product()
        {
            Name = string.Empty;
        }

        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            Price = p.Price;
            LegacyProperty1 = string.Empty;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}