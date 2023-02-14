using BLL.Models.Education;

namespace BLL.ValidatorInterfaces
{
    public interface IEducationValidator
    {
        List<string> Validate(EducationCreateAndUpdateDto educationUpdateDto);
    }
}
