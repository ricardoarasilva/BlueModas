using System;
namespace BlueModas.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public Guid OrderItemId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
