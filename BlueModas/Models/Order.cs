using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueModas.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public Guid OrderId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
        public decimal Quantity { get; set; }
        public bool InProgress { get; set; }
        public DateTime DateTimeOrder { get; set; }
        public Customer CustomerData { get; set; }

    }
}
