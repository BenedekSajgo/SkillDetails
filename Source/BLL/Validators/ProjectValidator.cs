using BLL.Models.Project;
using BLL.ValidatorInterfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class ProjectValidator : IProjectValidator
    {
        public List<string> Validate(ProjectCreateAndUpdateDto project)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(project.Title))
                validation.Add("Title is requried.");
            int titleLength = 256;
            if (project.Title.Length > titleLength) 
                validation.Add($"Title max length is {titleLength} characters");

            if (string.IsNullOrWhiteSpace(project.ShortDescription))
                validation.Add("Short description is requried.");
            int shortDescriptionLength = 256;
            if (project.ShortDescription.Length > shortDescriptionLength)
                validation.Add($"Short description max length is {shortDescriptionLength} characters");

            if (string.IsNullOrWhiteSpace(project.LongDescription))
                validation.Add("Long description is requried.");
            int longDescriptionLenght = 256;
            if (project.LongDescription.Length > longDescriptionLenght)
                validation.Add($"Long description max length is {longDescriptionLenght} characters");

            if (string.IsNullOrWhiteSpace(project.Function))
                validation.Add("Funtion is requried.");
            int functionLength = 256;
            if (project.Function.Length > functionLength)
                validation.Add($"Function max length is {functionLength} characters");

            if (project.StartDate > DateTime.Now)
                validation.Add("Start date can't be in the future");

            if (project.EndDate > DateTime.Now)
                validation.Add("End date can't be in the future");

            if (project.StartDate >= project.EndDate)
                validation.Add("End date can't be less than start date");

            return validation;
        }
    }
}
