using DapperGenericRepositoryProject.API.Models;

namespace DapperGenericRepositoryProject.API.DTOs
{
    public class ProductWithCategoryDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Category Category { get; set; }
    }
}
