using AutoMapper;
using BLL.Models;
using BLL.Models.Person;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;

namespace BLL.Managers
{
    public class PersonManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IPersonValidator _validator;

        public PersonManager(IPersonRepository personRepository, IMapper mapper, IPersonValidator validator)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResult<PersonDto>> GetPersonAsync(int id, CancellationToken cancellation)
        {
            var personEntity = await _personRepository.GetPersonAsync(id, cancellation);
            if (personEntity == null)
                return new BaseResult<PersonDto> { Errors = new List<string> { $"Person with Id {id} does not exist" } };

            var personDto = _mapper.Map<PersonDto>(personEntity);
            return new BaseResult<PersonDto> { Data = personDto };
        }

        public async Task<BaseResult<List<PersonSearchDto>>> GetPersonNamesAndIdsAsync(CancellationToken cancellation)
        {
            var personEntities = await _personRepository.GetPersonsAsync(false, cancellation);

            var personDtos = _mapper.Map<List<PersonSearchDto>>(personEntities);
            return new BaseResult<List<PersonSearchDto>> { Data = personDtos };
        }
        public async Task<BaseResult<List<PersonSearchDto>>> GetPersonNamesAndIdsIncludeDeletedAsync(CancellationToken cancellation)
        {
            var personEntities = await _personRepository.GetPersonsAsync(true, cancellation);

            var personDtos = _mapper.Map<List<PersonSearchDto>>(personEntities);
            return new BaseResult<List<PersonSearchDto>> { Data = personDtos };
        }

        public async Task<BaseResult<PersonDto>> CreatePersonAsync(PersonCreateAndUpdateDto personCreationDto, CancellationToken cancellation)
        {
            if (await _personRepository.PersonByNameAndMailExistsAsync(personCreationDto.LastName, personCreationDto.FirstName, personCreationDto.Email, cancellation))
                return new BaseResult<PersonDto> { Errors = new List<string> { "Person with this name already exists. Do you want to update existing person?" } };

            var errors = _validator.Validate(personCreationDto);
            if (errors.Any())
                return new BaseResult<PersonDto> { Errors = errors };

            var personEntity = _mapper.Map<Person>(personCreationDto);

            _personRepository.CreatePersonAsync(personEntity);
            await _personRepository.SaveChangesAsync(cancellation);

            var createdPersonDto = _mapper.Map<PersonDto>(personEntity);
            return new BaseResult<PersonDto> { Data = createdPersonDto };
        }

        public async Task<BaseResult<PersonDto>> UpdatePersonAsync(int id, PersonCreateAndUpdateDto personUpdateDto, CancellationToken cancellation)
        {
            var personEntity = await _personRepository.GetPersonAsync(id, cancellation);
            if (personEntity == null)
                return new BaseResult<PersonDto> { Errors = new List<string> { $"Person with Id {id} does not exist" } };

            var errors = _validator.Validate(personUpdateDto);
            if (errors.Any())
                return new BaseResult<PersonDto> { Errors = errors };

            _mapper.Map(personUpdateDto, personEntity);
            await _personRepository.SaveChangesAsync(cancellation);

            var updatedPerson = _mapper.Map<PersonDto>(personEntity);
            return new BaseResult<PersonDto> { Data = updatedPerson };
        }

        public async Task DeletePersonAsync(int id, CancellationToken cancellation)
        {
            await _personRepository.DeletePerson(id, cancellation);
            await _personRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrievePersonAsync(int id, CancellationToken cancellation)
        {
            await _personRepository.RetrievePerson(id, cancellation);
            await _personRepository.SaveChangesAsync(cancellation);
        }
    }
}
