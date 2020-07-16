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
        [HttpGet("last-orders")]
        public List<Order> LastOrders()
        {

            return _context.Orders.Where(a=>a.InProgress == false).Include("OrderItems.Product").Include("CustomerData").OrderByDescending(a=>a.DateTimeOrder).ToList();
        }

        [HttpPost("add-product/{value}")]
        public dynamic AddProduct(Guid value)
        {
            var model = _context.Orders.Include("OrderItems.Product").FirstOrDefault(a => a.InProgress);
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
            model.Quantity = model.OrderItems.Sum(a => a.Quantity);

            _context.SaveChanges();

            return new
            {
                Success = true,
                QuantityInCart = model.Quantity
            };
        }

        [HttpGet("get-cart")]
        public Order GetCart()
        {
            return _context.Orders.Include("OrderItems.Product").FirstOrDefault(a=>a.InProgress);
        }

        // POST api/values
        [HttpPost("update-cart-item")]
        public dynamic UpdateCartItem([FromBody] OrderItem value)
        {
            var model = _context.Orders.Include("OrderItems.Product").FirstOrDefault(a => a.InProgress);
            if(model != null)
            {
                var item = model.OrderItems.FirstOrDefault(a => a.OrderItemId == value.OrderItemId);
                item.Quantity = value.Quantity;

                model.Total = model.OrderItems.Sum(a => a.Quantity * a.Product.Price);
                model.Quantity = model.OrderItems.Sum(a => a.Quantity);

                _context.SaveChanges();
            }

            return new
            {
                Success = true,
                QuantityInCart = model.Quantity
            };
        }

        // DELETE api/values/5
        [HttpDelete("remove-item-cart/{id}")]
        public dynamic RemoveItemCart(Guid id)
        {
            var model = _context.Orders.Include("OrderItems.Product").FirstOrDefault(a => a.InProgress);
            if (model != null)
            {
                model.OrderItems.Remove(model.OrderItems.FirstOrDefault(a => a.OrderItemId == id));
                _context.OrderItems.Remove(_context.OrderItems.FirstOrDefault(a => a.OrderItemId == id));

                model.Total = model.OrderItems.Sum(a => a.Quantity * a.Product.Price);
                model.Quantity = model.OrderItems.Sum(a => a.Quantity);

                _context.SaveChanges();
            }

            return new
            {
                Success = true,
                QuantityInCart = model.Quantity
            };

        }

        // POST api/values
        [HttpPost("confirm-order")]
        public dynamic ConfirmOrder([FromBody] Order value)
        {
            var model = _context.Orders.Include("OrderItems.Product").Include("CustomerData").FirstOrDefault(a => a.InProgress);
            if (model != null)
            {
                model.CustomerData = value.CustomerData;
                model.InProgress = false;
                model.DateTimeOrder = DateTime.Now;

                model.Total = model.OrderItems.Sum(a => a.Quantity * a.Product.Price);
                model.Quantity = model.OrderItems.Sum(a => a.Quantity);

                _context.SaveChanges();
            }

            return new
            {
                Success = true,
                QuantityInCart = 0
            };
        }
    }
}
