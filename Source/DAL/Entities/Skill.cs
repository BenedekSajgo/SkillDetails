using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Skill : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public Category SkillCategory { get; set; }
        [Required]
        public List<SkillLevel> SkillLevelList { get; set; } = new();

        public List<Person> People { get; set; }
        public List<Project> Projects { get; set; }
    }
}
