using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _context;

        public OrderController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("add-product/{value}")]
        public void AddProduct(Guid value)
        {
            var model = _context.Orders.FirstOrDefault(a => a.InProgress);
            if(model == null)
            {
                model = new Order();
                model.InProgress = true;
                model.OrderId = Guid.NewGuid();
                _context.Orders.Add(model);
            }

            var item = model.OrderItems.FirstOrDefault(a => a.Product.ProductId == value);
            if(item == null)
            {
                item = new OrderItem();
                item.Product = _context.Products.Find(value);
                item.OrderItemId = Guid.NewGuid();
                _context.OrderItems.Add(item);
                model.OrderItems.Add(item);
            }

            item.Quantity += 1;

            model.Total = model.OrderItems.Sum(a => a.Quantity * a.Product.Price);

            _context.SaveChanges();
        }

        [HttpGet("get-cart")]
        public Order GetCart()
        {
            return _context.Orders.Include("OrderItems.Product").FirstOrDefault(a=>a.InProgress);
        }
    }
}
