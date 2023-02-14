using AutoMapper;
using BLL.Models;
using BLL.Models.Education;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BLL.Managers
{
    public class EducationManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IEducationValidator _educationValidator;

        public EducationManager(IPersonRepository personRepository, IMapper mapper, IEducationValidator educationValidator)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _educationValidator = educationValidator;
        }

        public async Task<BaseResult<EducationDto>> GetEducationAsync(int id, CancellationToken cancellation)
        {
            var educationEntity = await _personRepository.GetEducationAsync(id, cancellation);
            if (educationEntity == null) 
                return new BaseResult<EducationDto> { Errors = new List<string> { $"Education with Id {id} does not exist" } };

            var educationDto = _mapper.Map<EducationDto>(educationEntity);
            return new BaseResult<EducationDto> { Data = educationDto };
        }

        public async Task<BaseResult<List<EducationDto>>> GetDeletedEducationsForPersonAsync(int id, CancellationToken cancellation)
        {
            var educationEntities = await _personRepository.GetDeletedEducationsForPerson(id, cancellation);

            var educationDtos = _mapper.Map<List<EducationDto>>(educationEntities);
            return new BaseResult<List<EducationDto>> { Data = educationDtos };
        }

        public async Task<BaseResult<EducationDto>> CreateEducationAsync(int personId, EducationCreateAndUpdateDto educationUpdateDto, CancellationToken cancellationToken)
        {
            var errors = _educationValidator.Validate(educationUpdateDto);
            if (errors.Any())
                return new BaseResult<EducationDto> { Errors = errors };

            var educationEntity = _mapper.Map<Education>(educationUpdateDto);

            await _personRepository.CreateEducationAsync(personId, educationEntity, cancellationToken);
            await _personRepository.SaveChangesAsync(cancellationToken);

            var createdEducation = _mapper.Map<EducationDto>(educationEntity);
            return new BaseResult<EducationDto> { Data = createdEducation };
        }

        public async Task<BaseResult<EducationDto>> UpdateEducationAsync(int id, EducationCreateAndUpdateDto educationUpdateDto, CancellationToken cancellation)
        {
            var educationEntity = await _personRepository.GetEducationAsync(id, cancellation);
            if (educationEntity == null) 
                return new BaseResult<EducationDto> { Errors = new List<string> { $"Education with Id {id} does not exist" } };

            var errors = _educationValidator.Validate(educationUpdateDto);
            if (errors.Any())
                return new BaseResult<EducationDto> { Errors = errors };

            _mapper.Map(educationUpdateDto, educationEntity);
            await _personRepository.SaveChangesAsync(cancellation);

            var updatedEducation = _mapper.Map<EducationDto>(educationEntity);
            return new BaseResult<EducationDto> { Data = updatedEducation };
        }
        public async Task DeleteEducationAsync(int id, CancellationToken cancellation)
        {
            _personRepository.DeleteEducation(id);
            await _personRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrieveEducationAsync(int id, CancellationToken cancellation)
        {
            _personRepository.RetrieveEduation(id);
            await _personRepository.SaveChangesAsync(cancellation);
        }
    }
}
