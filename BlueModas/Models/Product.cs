﻿using System;
namespace BlueModas.Models
{
    public class Product
    {
        public Product()
        {
        }

        public long ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
