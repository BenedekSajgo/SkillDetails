using BLL.Models.Category;

namespace BLL.ValidatorInterfaces
{
    public interface ISkillCategoryValidator
    {
        List<string> Validate(CategoryCreateAndUpdateDto categoryUpdateDto);
    }
}
