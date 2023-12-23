using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperGenericRepositoryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = new List<Product>();
            products = _productRepository.GetAll().ToList();
            return Ok(products);
        }
        [HttpGet("{ProductId}")]
        public IActionResult Get(int ProductId)
        {
            Product product = new Product();
            product = _productRepository.GetById(ProductId);
            return Ok(product);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> OldestProducts()
        {
            List<Product> products = new List<Product>();
            products = await _productRepository.GetOldestProductsAsync();
            return Ok(products);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = false;
            isDeleted = _productRepository.Delete(id);
            return Ok(isDeleted);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            bool isAdded = false;
            isAdded = _productRepository.Add(product);
            return Ok(isAdded);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            bool isUpdated = false;
            isUpdated = _productRepository.Update(product);
            return Ok(isUpdated);
        }
    }
}
