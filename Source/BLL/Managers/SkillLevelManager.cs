using AutoMapper;
using BLL.Models;
using BLL.Models.SkillLevel;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BLL.Managers
{
    public class SkillLevelManager
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly ISkillLevelValidator _validator;

        public SkillLevelManager(ISkillRepository skillRepository, IMapper mapper, ISkillLevelValidator validator)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResult<SkillLevelDto>> GetSkillLevelAsync(int id, CancellationToken cancellation)
        {
            var skillLevelEntity = await _skillRepository.GetSkillLevelAsync(id, cancellation);
            if (skillLevelEntity == null)
                return new BaseResult<SkillLevelDto> { Errors = new List<string> { $"SkillLevel with Id {id} does not exist" } };

            var skillLevelDto = _mapper.Map<SkillLevelDto>(skillLevelEntity);
            return new BaseResult<SkillLevelDto> { Data = skillLevelDto };
        }

        public async Task<BaseResult<List<SkillLevelDto>>> GetDeletedSkillLevelsForSkillAsync(int skillId, CancellationToken cancellation)
        {
            var skillLevelEntities = await _skillRepository.GetSkillLevelsForSkillAsync(skillId, true, cancellation);

            var skillLevelDtos = _mapper.Map<List<SkillLevelDto>>(skillLevelEntities);
            return new BaseResult<List<SkillLevelDto>> { Data = skillLevelDtos };
        }

        public async Task<BaseResult<SkillLevelDto>> CreateSkillLevelAsync(int skillId, SkillLevelCreateAndUpdateDto skillLevelCreateDto, CancellationToken cancellation)
        {
            // TODO CreateSkillLevel check if skilllevel with name already exists

            var errors = _validator.Validate(skillLevelCreateDto);
            if (errors.Any())
                return new BaseResult<SkillLevelDto> { Errors = errors };

            var skillLevelEntity = _mapper.Map<SkillLevel>(skillLevelCreateDto);

            await _skillRepository.CreateSkillLevelAsync(skillId, skillLevelEntity, cancellation);
            await _skillRepository.SaveChangesAsync(cancellation);

            var createdSkillLevel = _mapper.Map<SkillLevelDto>(skillLevelEntity);
            return new BaseResult<SkillLevelDto> { Data = createdSkillLevel }; 
        }

        public async Task<BaseResult<SkillLevelDto>> UpdateSkillLevelAsync(int id, int skillId, SkillLevelCreateAndUpdateDto skillLevelUpdateDto, CancellationToken cancellation)
        {
            var skillLevelEntity = await _skillRepository.GetSkillLevelAsync(id, cancellation);
            if (skillLevelEntity == null)
                return new BaseResult<SkillLevelDto> { Errors = new List<string> { $"SkillLevel with Id {id} does not exist"} };

            var errors = _validator.Validate(skillLevelUpdateDto);
            if (errors.Any())
                return new BaseResult<SkillLevelDto> { Errors = errors };

            _mapper.Map(skillLevelUpdateDto, skillLevelEntity);
            await _skillRepository.SaveChangesAsync(cancellation);

            await _skillRepository.UpdateSkillLevelOrderAsync(skillId, _mapper.Map<SkillLevel>(skillLevelUpdateDto), cancellation);
            await _skillRepository.ResetSkillLevelOrderAsync(skillId, cancellation);
            await _skillRepository.SaveChangesAsync(cancellation);

            var updatedSkillLevel = _mapper.Map<SkillLevelDto>(skillLevelEntity);
            return new BaseResult<SkillLevelDto> { Data = updatedSkillLevel }; 
        }

        public async Task DeleteSkillLevelAsync(int id, CancellationToken cancellation)
        {
            _skillRepository.DeleteSkillLevel(id);
            await _skillRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrieveSkillLevelAsync(int id, CancellationToken cancellation)
        {
            _skillRepository.RetrieveSkillLevel(id);
            await _skillRepository.SaveChangesAsync(cancellation);
        }
    }
}
