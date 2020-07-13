using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueModas.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private List<Product> _productList;

        public ProductController()
        {
            _productList = new List<Product>();

            _productList.Add(new Product { Description = "Produto 1", Image = "", Price = 10, ProductId = 1 });
            _productList.Add(new Product { Description = "Produto 2", Image = "", Price = 10, ProductId = 2 });
            _productList.Add(new Product { Description = "Produto 3", Image = "", Price = 10, ProductId = 3 });
            _productList.Add(new Product { Description = "Produto 4", Image = "", Price = 10, ProductId = 4 });
            _productList.Add(new Product { Description = "Produto 5", Image = "", Price = 10, ProductId = 5 });
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(long id)
        {
            return _productList.FirstOrDefault(a => a.ProductId == id);
        }
    }
}
