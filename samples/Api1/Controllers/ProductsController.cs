using Api1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return new[] { new Product(), new Product() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Product> Get(string id)
        {
            return new Product { Id = id };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Product value)
        {
        }
    }
}
