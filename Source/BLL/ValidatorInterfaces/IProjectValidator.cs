using BLL.Models.Project;

namespace BLL.ValidatorInterfaces
{
    public interface IProjectValidator
    {
        List<string> Validate(ProjectCreateAndUpdateDto projectUpdateDto);
    }
}
