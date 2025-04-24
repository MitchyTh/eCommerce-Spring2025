using Api.eCommerce.Database;
using API.eCommerce.EC;
using API.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public ReceiptController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Receipt Get()
        {
            return new ReceiptEC().Get();
        }

        [HttpPost("purchase")]
        public Models.Receipt FinalizePurchase()
        {
            var receipt = new ReceiptEC().FinalizePurchase();
            return receipt;
        }
    }
}
