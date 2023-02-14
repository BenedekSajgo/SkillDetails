using AutoMapper;
using BLL.Models.SkillLevel;
using DAL.Entities;

namespace BLL.Profiles
{
    public class SkillLevelProfile : Profile
    {
        public SkillLevelProfile()
        {
            CreateMap<SkillLevel, SkillLevelDto>().ReverseMap();
            CreateMap<SkillLevelCreateAndUpdateDto, SkillLevel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        }
    }
}
