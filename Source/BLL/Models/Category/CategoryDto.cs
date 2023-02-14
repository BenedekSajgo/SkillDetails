using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Category
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public CategoryDto? ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<CategoryDto> ChildCategories { get; set; }
        public int Order { get; set; }
    }
}
