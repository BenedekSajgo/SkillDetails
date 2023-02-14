using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class SkillLevel : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public string Picture { get; set; } 
    }
}
