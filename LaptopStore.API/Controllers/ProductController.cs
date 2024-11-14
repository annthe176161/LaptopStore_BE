using LaptopStore.Business.DTOs;
using LaptopStore.Business.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // GET: api/products/brand/{brandId}
        [HttpGet("brand/{brandId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByBrand(int brandId)
        {
            var products = await _productService.GetProductsByBrandAsync(brandId);
            return Ok(products);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<ProductDTO> AddProduct([FromBody] ProductDTO productDto)
        {
            var result = _productService.Add(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.ProductID }, productDto);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.ProductID)
            {
                return BadRequest();
            }

            _productService.Update(productDto);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Delete(id);
            return NoContent();
        }
    }
}
