using BLL.Models.Skill;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class SkillValidator : ISkillValidator
    {
        public List<string> Validate(SkillCreateAndUpdateDto skill)
        {
            List<string> validation = new();

            if (string.IsNullOrWhiteSpace(skill.Name))
                validation.Add("Name is required.");
            int nameLength = 256;
            if (skill.Name.Length > nameLength)
                validation.Add($"Name max length is {nameLength} characters");

            if (skill.Order < 0)
                validation.Add("Order can't be negative");

            return validation;
        }
    }
}
