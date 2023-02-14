using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace DAL.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Picture { get; set; }

        public List<Skill> Skills { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Education> Educations { get; set; } = new();
    }
}
