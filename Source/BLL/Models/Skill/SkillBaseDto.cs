using BLL.Models.Category;
using BLL.Models.SkillLevel;

namespace BLL.Models.Skill
{
    public class SkillBaseDto : BaseDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public CategoryDto SkillCategory { get; set; }
        public List<SkillLevelDto> SkillLevelList { get; set; } = new();
    }
}
