using AutoMapper;
using BLL.Models.Person;
using DAL.Entities;

namespace BLL.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, PersonSearchDto>();
            CreateMap<PersonCreateAndUpdateDto, Person>()
                //.ForMember(dest => dest.DateOfBirth, opt => opt.)65644545
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore())
                .ForMember(dest => dest.Educations, opt => opt.Ignore());
        }
    }
}
