using AutoMapper;
using BLL.Models.Education;
using DAL.Entities;

namespace BLL.Profiles
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {
            CreateMap<Education, EducationDto>();
            CreateMap<EducationCreateAndUpdateDto, Education>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());        
        }
    }
}
