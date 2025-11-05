using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Data;

namespace SuperShop.Controllers.API
{
    public class ProductsController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]

        public class ProductController : Controller
        {
            private readonly IProductRepository _productRepository;

            public ProductController(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public IActionResult GetProducts()
            {
                return View();
            }
        }
    }
}
