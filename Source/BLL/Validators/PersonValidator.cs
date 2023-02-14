using BLL.Models.Person;
using BLL.ValidatorInterfaces;
using System.ComponentModel.DataAnnotations;
using Util.Enums;

namespace BLL.Validators
{
    public class PersonValidator : IPersonValidator
    {
        private readonly List<string> _imageExtensions = new() { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };

        public List<string> Validate(PersonCreateAndUpdateDto person)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(person.FirstName))
                validation.Add("First name is required.");
            int firstNameLength = 256;
            if (person.FirstName.Length > firstNameLength)
                validation.Add($"First name max length is {firstNameLength} characters.");

            if (string.IsNullOrWhiteSpace(person.LastName))
                validation.Add("Last name is required.");
            int lastNameLegnth = 256;
            if (person.LastName.Length > lastNameLegnth)
                validation.Add($"Last name max length is {lastNameLegnth} characters.");

            if (!new EmailAddressAttribute().IsValid(person.Email))
                validation.Add("Email address is not valid.");

            if (DateTime.Today.AddYears(-18) <= person.DateOfBirth)
                validation.Add("User has to be older than 18.");

            if (!Enum.IsDefined(typeof(Gender), person.Gender))
            //if (!Enum.TryParse(_person.Gender.ToString().ToUpper(), out Gender gender))
                validation.Add("Gender is not valid (options are M/V/X).");

            if (string.IsNullOrWhiteSpace(person.ShortDescription))
                validation.Add("Short description is required.");
            int shortDescriptionLength = 256;
            if (person.ShortDescription.Length > shortDescriptionLength)
                validation.Add($"Short description max length is {shortDescriptionLength} characters.");

            if (!_imageExtensions.Contains(Path.GetExtension(person.Picture).ToLower()))
                validation.Add("Picture file is not supported.");

            return validation;
        }
    }
}
