namespace BLL.Models.Skill
{
    public class SkillCreateAndUpdateDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int CategoryId { get; set; }
        //public List<SkillLevelDto> SkillLevelList { get; set; } = new();
    }
}
