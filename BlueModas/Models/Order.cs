using System;
using System.Collections.Generic;

namespace BlueModas.Models
{
    public class Order
    {
        public Order()
        {

        }

        public long OrderNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }

    }
}
