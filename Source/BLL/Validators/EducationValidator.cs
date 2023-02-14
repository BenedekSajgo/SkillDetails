using BLL.Models.Education;
using BLL.ValidatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class EducationValidator : IEducationValidator
    {
        public List<string> Validate(EducationCreateAndUpdateDto education)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(education.Title))
                validation.Add("Title is required.");
            int titleLength = 256;
            if (education.Title.Length > titleLength)
                validation.Add($"Title max length is {titleLength} characters");

            if (string.IsNullOrWhiteSpace(education.Body))
                validation.Add("Body is required.");
            int bodyLength = 256;
            if (education.Body.Length > bodyLength)
                validation.Add($"Body max length is {bodyLength} characters");

            if (string.IsNullOrWhiteSpace(education.ShortDescription))
                validation.Add("Short description is required.");
            int shortDescriptionLength = 256;
            if (education.ShortDescription.Length > shortDescriptionLength)
                validation.Add($"Short description max length is {shortDescriptionLength} characters");

            if (education.StartDate > DateTime.Now)
                validation.Add("Start date can't be in the future.");

            if (education.EndDate > DateTime.Now)
                validation.Add("End date can't be in the turue");

            if (education.StartDate >= education.EndDate)
                validation.Add("End date can't be less than start date.");

            return validation;
        }
    }
}
