using AutoMapper;
using BLL.Models.Skill;
using DAL.Entities;

namespace BLL.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillDto>();
            CreateMap<Skill, SkillBaseDto>();
            CreateMap<SkillCreateAndUpdateDto, Skill>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.SkillCategory, opt => opt.Ignore())
                .ForMember(dest => dest.SkillLevelList, opt => opt.Ignore())
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore());
        }
    }
}
