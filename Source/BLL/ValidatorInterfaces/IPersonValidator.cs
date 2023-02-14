using BLL.Models.Person;

namespace BLL.ValidatorInterfaces
{
    public interface IPersonValidator
    {
        List<string> Validate(PersonCreateAndUpdateDto personCreateAndUpdateDto);
    }
}
