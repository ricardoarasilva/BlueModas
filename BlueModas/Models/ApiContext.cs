using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Models
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        {
            AddTestData();
        }

        private void AddTestData()
        {
            if (!Products.Any())
            {
                Products.Add(new Product { Description = "Produto 1", Image = "", Price = 10, ProductId = System.Guid.NewGuid() });
                Products.Add(new Product { Description = "Produto 2", Image = "", Price = 20, ProductId = System.Guid.NewGuid() });
                Products.Add(new Product { Description = "Produto 3", Image = "", Price = 30, ProductId = System.Guid.NewGuid() });
                Products.Add(new Product { Description = "Produto 4", Image = "", Price = 40, ProductId = System.Guid.NewGuid() });
                Products.Add(new Product { Description = "Produto 5", Image = "", Price = 50, ProductId = System.Guid.NewGuid() });
                this.SaveChanges();
            }
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
