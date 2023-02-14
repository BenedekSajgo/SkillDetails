namespace BLL.Models.Category
{
    public class CategoryCreateAndUpdateDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int? ParentCategoryId { get; set; }
        //public CategoryCreateAndUpdateDto? ParentCategory { get; set; }
        //public List<CategoryCreateAndUpdateDto> ChildCategories { get; set; }
    }
}
