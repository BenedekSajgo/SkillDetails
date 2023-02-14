using DAL.Entities;

namespace DAL.RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellation);
        Task<bool> SkillCategoryExistsByIdAsync(int? id, CancellationToken cancellation);
        Task<bool> SkillCategoryExistsByNameAsync(string name, CancellationToken cancellation);
        Task<Category?> GetSkillCategoryAsync(int id, CancellationToken cancellation);
        Task<Category?> GetSkillCategoryWithChildrenAsync(int id, CancellationToken cancellation);
        Task<Category?> GetParentCategoryAsync(int id, CancellationToken cancellation);
        Task<IEnumerable<Category>> GetChildCategoriesAsync(int  id, CancellationToken cancellation);
        Task<IEnumerable<Category>> GetParentCategoriesFromSameCategoryLevelAsync(int? categoryId, CancellationToken cancellation);
        Task CreateSkillCategoryAsync(Category skillCategory, CancellationToken cancellation);
        Task DeleteSkillCategoryAsync(int id, CancellationToken cancellation);
        Task RetrieveSkillCategoryAsync(int id, CancellationToken cancellation);

    }
}
