using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }
        public Category? ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<Category> ChildCategories { get; set; } = new();
    }
}
