using BLL.Models.SkillLevel;

namespace BLL.ValidatorInterfaces
{
    public interface ISkillLevelValidator
    {
        List<string> Validate(SkillLevelCreateAndUpdateDto skillLevelUpdateDto);
    }
}
