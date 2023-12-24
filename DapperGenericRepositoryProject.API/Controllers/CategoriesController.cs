using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperGenericRepositoryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categories = new List<Category>();
            categories = _unitOfWork.Categories.GetAll().ToList();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = new Category();
            category = _unitOfWork.Categories.GetById(id);
            return Ok(category);
        }      
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = false;
            isDeleted = _unitOfWork.Categories.Delete(id);
            return Ok(isDeleted);
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            bool isAdded = false;
            isAdded = _unitOfWork.Categories.Add(category);
            return Ok(isAdded);
        }
        [HttpPut]
        public IActionResult Update(Category category)
        {
            bool isUpdated = false; 
            isUpdated = _unitOfWork.Categories.Update(category);
            return Ok(isUpdated);
        }
    }
}
