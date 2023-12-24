using DapperGenericRepositoryProject.API.DTOs;
using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperGenericRepositoryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = new List<Product>();
            products = _unitOfWork.Products.GetAll().ToList();
            return Ok(products);
        }
        [HttpGet("{ProductId}")]
        public IActionResult Get(int ProductId)
        {
            Product product = new Product();
            product = _unitOfWork.Products.GetById(ProductId);
            return Ok(product);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> OldestProducts()
        {
            List<Product> products = new List<Product>();
            products = await _unitOfWork.Products.GetOldestProductsAsync();
            return Ok(products);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ProductWithCategory()
        {
            List<ProductWithCategoryDto> products = new List<ProductWithCategoryDto>();
            products = await _unitOfWork.Products.GetProductWithCategoryAsync();
            return Ok(products);
        }
            
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = false;
            isDeleted = _unitOfWork.Products.Delete(id);
            return Ok(isDeleted);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            bool isAdded = false;
            isAdded = _unitOfWork.Products.Add(product);
            return Ok(isAdded);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            bool isUpdated = false;
            isUpdated = _unitOfWork.Products.Update(product);
            return Ok(isUpdated);
        }
    }
}
