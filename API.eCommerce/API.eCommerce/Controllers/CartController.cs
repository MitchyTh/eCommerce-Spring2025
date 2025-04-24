using Api.eCommerce.Database;
using API.eCommerce.Database;
using API.eCommerce.EC;
using API.eCommerce.Models;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {

        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item?> Get()
        {
            return new CartEC().Get();
        }

        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            return new CartEC().Get()
                .FirstOrDefault(i => i?.Id == id);
        }

        [HttpPost("add")]
        public Item? AddOrUpdate([FromBody]Item item)
        {
            var cartItem = new CartEC().AddOrUpdate(item);
            return item;
        }

        [HttpPost("return")]
        public Item? ReturnItem([FromBody] Item item)
        {
            var cartItem = new CartEC().ReturnItem(item);
            return item;
        }
    }
}
