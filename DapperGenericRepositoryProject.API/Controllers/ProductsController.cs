using DapperGenericRepositoryProject.API.Models;
using DapperGenericRepositoryProject.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperGenericRepositoryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = new List<Product>();

            ProductRepository productsRepository = new ProductRepository();
            products=productsRepository.GetAll().ToList();

            return Ok(products);
        }
        [HttpGet("{ProductId}")]
        public IActionResult Get(int ProductId)
        {
            Product product = new Product();
            ProductRepository productRepository = new ProductRepository();
            product = productRepository.GetById(ProductId);
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = false;
            ProductRepository productRepository = new ProductRepository();
            isDeleted = productRepository.Delete(id);
            return Ok(isDeleted);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            bool isAdded = false;
            try
            {
                ProductRepository productRepository = new ProductRepository();
                isAdded = productRepository.Add(product);
            }
            catch (Exception ex)
            {
            }
            return Ok(isAdded);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            bool isUpdated = false;
            try
            {
                ProductRepository productRepository = new ProductRepository();
                isUpdated = productRepository.Update(product);
            }
            catch (Exception ex)
            {
            }

            return Ok(isUpdated);
        }
    }
}
