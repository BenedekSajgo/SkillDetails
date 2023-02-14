using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Project : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public string Function { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public List<Skill> Skills { get; set; } = new();
    }
}
