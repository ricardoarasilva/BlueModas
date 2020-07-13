using System;
namespace BlueModas.Models
{
    public class Customer
    {
        public Customer()
        {
        }
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
