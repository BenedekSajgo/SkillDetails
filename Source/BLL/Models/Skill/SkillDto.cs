using System.ComponentModel.DataAnnotations;
using BLL.Models.Category;
using BLL.Models.Person;
using BLL.Models.Project;
using BLL.Models.SkillLevel;

namespace BLL.Models.Skill
{
    public class SkillDto : BaseDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public CategoryDto SkillCategory { get; set; }
        public List<SkillLevelDto> SkillLevelList { get; set; } = new();

        public List<PersonDto> People { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = new();


        public void AddSkillLevel(SkillLevelDto skillLevel)
        {
            SkillLevelList.Add(skillLevel);
        }

        public void AddPerson(PersonDto person)
        {
            People.Add(person);
        }

        public void AddProject(ProjectDto project)
        {
            Projects.Add(project);
        }
    }
}
