using AutoMapper;
using BLL.Models;
using BLL.Models.Category;
using BLL.ValidatorInterfaces;
using DAL.Entities;
using DAL.RepositoryInterfaces;

namespace BLL.Managers
{
    public class CategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISkillCategoryValidator _validator;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, ISkillCategoryValidator validator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResult<CategoryDto>> GetSkillCategoryAsync(int id, CancellationToken cancellation)
        {
            var categoryEntity = await _categoryRepository.GetSkillCategoryWithChildrenAsync(id, cancellation);
            if (categoryEntity == null)
                return new BaseResult<CategoryDto> { Errors = new List<string> { $"Skill Category with Id {id} does not exist" } };

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
            return new BaseResult<CategoryDto> { Data = categoryDto };
        }

        public async Task<BaseResult<CategoryDto>> GetParentCategoryAsync(int childId, CancellationToken cancellation)
        {
            var parentEntity = await _categoryRepository.GetParentCategoryAsync(childId, cancellation);
            if (parentEntity == null)
                return new BaseResult<CategoryDto> { Errors = new List<string> { "This Skill Category does not have a Parent Category" } };

            var categoryDto = _mapper.Map<CategoryDto>(parentEntity);
            return new BaseResult<CategoryDto> { Data = categoryDto };
        }

        public async Task<BaseResult<List<CategoryDto>>> GetChildCategoriesAsync(int id, CancellationToken cancellation)
        {
            var categoryEntities = await _categoryRepository.GetChildCategoriesAsync(id, cancellation);
            if (!categoryEntities.Any())
                return new BaseResult<List<CategoryDto>> { Errors = new List<string> { $"Catogery with Id {id} doesnt have any Child Categories" } };

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categoryEntities);
            return new BaseResult<List<CategoryDto>> { Data = categoryDtos };
        }

        public async Task<BaseResult<List<CategoryDto>>> GetParentCategoriesAsync(CancellationToken cancellation)
        {
            var categoryEntities = await _categoryRepository.GetParentCategoriesFromSameCategoryLevelAsync(null, cancellation);

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categoryEntities);
            return new BaseResult<List<CategoryDto>> { Data = categoryDtos };
        }

        public async Task<BaseResult<CategoryDto>> CreateSkillCategoryAsync(CategoryCreateAndUpdateDto skillCategoryCreationDto, CancellationToken cancellation)
        {
            if (await _categoryRepository.SkillCategoryExistsByNameAsync(skillCategoryCreationDto.Name, cancellation))
                return new BaseResult<CategoryDto> { Errors = new List<string> { "Skill already exists" } };

            if (skillCategoryCreationDto.ParentCategoryId != null)
                if (!await _categoryRepository.SkillCategoryExistsByIdAsync(skillCategoryCreationDto.ParentCategoryId, cancellation))
                    return new BaseResult<CategoryDto> { Errors = new List<string> { "Parent category Id doesn't exist" } }; 

            var errors = _validator.Validate(skillCategoryCreationDto);
            if (errors.Any())
                return new BaseResult<CategoryDto> { Errors = errors };

            var toUpdateOrderEntities = (await _categoryRepository.GetParentCategoriesFromSameCategoryLevelAsync(skillCategoryCreationDto.ParentCategoryId, cancellation)).ToList();
            foreach(var category in toUpdateOrderEntities)
                if (category.Order >= skillCategoryCreationDto.Order)
                    category.Order++; 
            await _categoryRepository.SaveChangesAsync(cancellation);

            var categoryEntity = _mapper.Map<Category>(skillCategoryCreationDto);
            await _categoryRepository.CreateSkillCategoryAsync(categoryEntity, cancellation);
            await _categoryRepository.SaveChangesAsync(cancellation);

            var resetOrderEntities = (await _categoryRepository.GetParentCategoriesFromSameCategoryLevelAsync(skillCategoryCreationDto.ParentCategoryId, cancellation)).ToList();
            for (int i = 0; i < resetOrderEntities.Count; i++)
                resetOrderEntities[i].Order = i++;
            await _categoryRepository.SaveChangesAsync(cancellation);

            var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);
            return new BaseResult<CategoryDto> { Data = createdCategory };
        }

        public async Task<BaseResult<CategoryDto>> UpdateSkillCategoryAsync(int id, CategoryCreateAndUpdateDto skillCategoryUpdateDto, CancellationToken cancellation)
        {
            if (id == skillCategoryUpdateDto.ParentCategoryId)
                return new BaseResult<CategoryDto> { Errors = new List<string> { "It's not possible to set a category itself as its own parent category" } };

            if (skillCategoryUpdateDto.ParentCategoryId != null)
                if (!await _categoryRepository.SkillCategoryExistsByIdAsync(skillCategoryUpdateDto.ParentCategoryId, cancellation))
                    return new BaseResult<CategoryDto> { Errors = new List<string> { "Parent category Id doesn't exist" } };

            var categoryEntity = await _categoryRepository.GetSkillCategoryAsync(id, cancellation);
            if (categoryEntity == null)
                return new BaseResult<CategoryDto> { Errors = new List<string> { $"Skill Category with Id {id} does not exist" } };

            if (await _categoryRepository.SkillCategoryExistsByNameAsync(skillCategoryUpdateDto.Name, cancellation))
                if (skillCategoryUpdateDto.Name != categoryEntity.Name)
                    return new BaseResult<CategoryDto> { Errors = new List<string> { "Skill already exists" } };

            var errors = _validator.Validate(skillCategoryUpdateDto);
            if (errors.Any())
                return new BaseResult<CategoryDto> { Errors = errors };

            _mapper.Map(skillCategoryUpdateDto, categoryEntity);
            await _categoryRepository.SaveChangesAsync(cancellation);

            var updatedCategory = _mapper.Map<CategoryDto>(categoryEntity);
            return new BaseResult<CategoryDto> { Data = updatedCategory };
        }

        public async Task DeleteCategoryAsync(int id, CancellationToken cancellation)
        {
            await _categoryRepository.DeleteSkillCategoryAsync(id, cancellation);
            await _categoryRepository.SaveChangesAsync(cancellation);
        }

        public async Task RetrieveCategoryAsync(int id, CancellationToken cancellation)
        {
            await _categoryRepository.RetrieveSkillCategoryAsync(id, cancellation);
            await _categoryRepository.SaveChangesAsync(cancellation);
        }
    }
}
