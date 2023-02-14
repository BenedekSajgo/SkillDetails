using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public abstract class BaseDto
    {
        public int Id { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
