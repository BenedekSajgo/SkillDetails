using BLL.Models.Education;
using BLL.Models.Project;
using BLL.Models.Skill;
using Util.Enums;

namespace BLL.Models.Person
{
    public class PersonDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }

        public List<SkillBaseDto> Skills { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = new();
        public List<EducationDto> Educations { get; set; } = new();


        public void AddSkill(SkillBaseDto skill)
        {
            Skills.Add(skill);
        }

        public void AddProject(ProjectDto project)
        {
            Projects.Add(project);
        }

        public void AddEducation(EducationDto education)
        {
            Educations.Add(education);
        }
    }
}
