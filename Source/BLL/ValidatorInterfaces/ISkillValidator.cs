using BLL.Models.Skill;
using BLL.Models.SkillLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ValidatorInterfaces
{
    public interface ISkillValidator
    {
        List<string> Validate(SkillCreateAndUpdateDto skillCreateAndUpdateDto);
    }
}
