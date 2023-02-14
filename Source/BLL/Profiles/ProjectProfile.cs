using AutoMapper;
using BLL.Models.Project;
using DAL.Entities;

namespace BLL.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectCreateAndUpdateDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Skills, opt => opt.Ignore());
        }
    }
}
