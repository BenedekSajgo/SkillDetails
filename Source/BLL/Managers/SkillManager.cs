using AutoMapper;
using BLL.Models;
using BLL.Models.Skill;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLL.Managers
{
    public class SkillManager
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISkillValidator _skillValidator;

        public SkillManager(ISkillRepository skillRepository, ICategoryRepository categoryRepository, IMapper mapper, ISkillValidator skillValidator)
        {
            _skillRepository = skillRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _skillValidator = skillValidator;
        }

        public async Task<BaseResult<SkillDto>> GetSkillAsync(int id, CancellationToken cancellation)
        {
            var skillEntity = await _skillRepository.GetSkillAsync(id, cancellation);
            if (skillEntity == null)
                return new BaseResult<SkillDto> { Errors = new List<string> { $"Skill with Id {id} does not exist" } };

            var skillDto = _mapper.Map<SkillDto>(skillEntity);
            return new BaseResult<SkillDto> { Data = skillDto };
        }

        public async Task<BaseResult<List<SkillDto>>> GetSkillsAsync(CancellationToken cancellation)
        {
            var skillEntities = await _skillRepository.GetSkillsAsync(cancellation);

            var skillDtos = _mapper.Map<List<SkillDto>>(skillEntities);
            return new BaseResult<List<SkillDto>> { Data = skillDtos };
        }

        public async Task<BaseResult<List<SkillDto>>> GetDeletedSkillsForPersonAsync(int personId, CancellationToken cancellation)
        {
            var skillEntities = await _skillRepository.GetDeletedSkillsForPersonAsync(personId, cancellation);

            var skillDtos = _mapper.Map<List<SkillDto>>(skillEntities);
            return new BaseResult<List<SkillDto>> { Data = skillDtos };
        }

        public async Task<BaseResult<SkillDto>> CreateSkillAsync(SkillCreateAndUpdateDto skillCreateDto, CancellationToken cancellation)
        {
            // TODO ervoor zorgen dat je verder kan doen na check
            var doesSkillExist = await _skillRepository.SkillExistsAsync(skillCreateDto.Name, cancellation);
            if (doesSkillExist)
                return new BaseResult<SkillDto> { Errors = new List<string> { "Database already contains an entity with this name.\nDo you want to overwrite the existing entity?" } };

            var errors = _skillValidator.Validate(skillCreateDto);
            if (errors.Any())
                return new BaseResult<SkillDto> { Errors = errors };

            var skillEntity = _mapper.Map<Skill>(skillCreateDto);
            skillEntity.SkillCategory = await _categoryRepository.GetSkillCategoryAsync(skillCreateDto.CategoryId, cancellation);

            await _skillRepository.CreateSkillAsync(skillEntity, cancellation);
            await _skillRepository.SaveChangesAsync(cancellation);

            var createdSkill = _mapper.Map<SkillDto>(skillEntity);
            return new BaseResult<SkillDto> { Data = createdSkill };
        }

        public async Task<BaseResult<SkillDto>> UpdateSkillAsync(int id, SkillCreateAndUpdateDto skillUpdateDto, CancellationToken cancellation)
        {
            var skillEntity = await _skillRepository.GetSkillAsync(id, cancellation);
            if (skillEntity == null)
                return new BaseResult<SkillDto> { Errors = new List<string> { $"Skill with Id {id} does not exist" } };

            var errors = _skillValidator.Validate(skillUpdateDto);
            if (errors.Any())
                return new BaseResult<SkillDto> { Errors = errors };

            skillEntity.SkillCategory = await _categoryRepository.GetSkillCategoryAsync(skillUpdateDto.CategoryId, cancellation);
            //skillEntity.SkillLevelList = await _skillRepository.GetSkillLevelsForSkillAsync(id, true, cancellation);

            _mapper.Map(skillUpdateDto, skillEntity);
            await _skillRepository.SaveChangesAsync(cancellation);

            var updatedSkill = _mapper.Map<SkillDto>(skillEntity);
            return new BaseResult<SkillDto> { Data = updatedSkill };
        }

        public async Task DeleteSkillAsync(int id, CancellationToken cancellation)
        {
            await _skillRepository.DeleteSkillAsync(id, cancellation);
            await _skillRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrieveSkillAsync(int id, CancellationToken cancellation)
        {
            await _skillRepository.RetrieveSkillAsync(id, cancellation);
            await _skillRepository.SaveChangesAsync(cancellation);
        }
    }
}
