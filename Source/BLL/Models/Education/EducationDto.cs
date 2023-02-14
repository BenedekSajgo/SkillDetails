using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Education
{
    public class EducationDto : BaseDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
