using BLL.Models.Category;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class SkillCategoryValidator : ISkillCategoryValidator
    {
        public List<string> Validate(CategoryCreateAndUpdateDto category)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(category.Name))
                validation.Add("Name is required.");
            int nameLength = 256;
            if (category.Name.Length > nameLength)
                validation.Add($"Name max length is {nameLength} characters");

            if (category.Order < 0)
                validation.Add("Order can't be negative");

            return validation;
        }
    }
}
