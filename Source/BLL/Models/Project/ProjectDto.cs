using BLL.Models.Skill;

namespace BLL.Models.Project
{
    public class ProjectDto : BaseDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Function { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<SkillBaseDto> Skills { get; set; } = new();


        public void AddSkill(SkillBaseDto skill)
        {
            Skills.Add(skill);
        }
    }
}
