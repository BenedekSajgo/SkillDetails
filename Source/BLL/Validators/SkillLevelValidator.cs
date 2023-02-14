using BLL.Models.SkillLevel;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class SkillLevelValidator : ISkillLevelValidator
    {
        private readonly List<string> _imageExtensions = new() { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };

        public List<string> Validate(SkillLevelCreateAndUpdateDto skillLevel)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(skillLevel.Name))
                validation.Add("Name is required.");
            int nameLength = 256;
            if (skillLevel.Name.Length > nameLength)
                validation.Add($"Name max length is {nameLength} characters");

            if (skillLevel.Order < 0)
                validation.Add("Order can't be negative");

            if (!_imageExtensions.Contains(Path.GetExtension(skillLevel.Picture).ToLower()))
                validation.Add("Picture file is not supported.");

            return validation;
        }
    }
}
