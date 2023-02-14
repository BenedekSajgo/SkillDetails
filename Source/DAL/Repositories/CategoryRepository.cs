using DAL.Data;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CVContext _context;

        public CategoryRepository(CVContext context)
        {
            _context = context;
        }

        public async Task<bool> SkillCategoryExistsByIdAsync(int? id, CancellationToken cancellation)
        {
            return await _context.SkillCategories.AnyAsync(s => s.Id == id, cancellation);
        }

        public async Task<bool> SkillCategoryExistsByNameAsync(string name, CancellationToken cancellation)
        {
            return await _context.SkillCategories.AnyAsync(s => s.Name == name, cancellation);
        }

        public async Task<Category?> GetSkillCategoryAsync(int id, CancellationToken cancellation)
        {
            return await _context.SkillCategories.Where(s => s.Id == id).FirstOrDefaultAsync(cancellation); //.Include(s => s.ParentCategory)
        }

        public async Task<Category?> GetSkillCategoryWithChildrenAsync(int id, CancellationToken cancellation)
        {// TODO lazyloading
            return await _context.SkillCategories
                .Where(c => c.Id == id && c.IsDeleted == false)
                .Include(c => c.ChildCategories.Where(c => c.IsDeleted == false).OrderBy(c => c.Order))
                //.ThenInclude(c => c.ChildCategories.Where(c => c.IsDeleted == false).OrderBy(c => c.Order))           //lazyloading om ALLES op te halen
                .FirstOrDefaultAsync(cancellation); 
        }

        public async Task<Category?> GetParentCategoryAsync(int childId, CancellationToken cancellation)
        {
            return await _context.SkillCategories
                .Where(c => c.Id == childId && c.IsDeleted == false)
                .Select(c => c.ParentCategory)
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<IEnumerable<Category>> GetChildCategoriesAsync(int id, CancellationToken cancellation)
        {
            return await _context.SkillCategories
                .Where(c => c.Id == id && c.IsDeleted == false)
                .SelectMany(c => c.ChildCategories)
                .OrderBy(c => c.Order)
                .ToListAsync(cancellation);
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesFromSameCategoryLevelAsync(int? categoryId, CancellationToken cancellation)
        {
            return await _context.SkillCategories
                .Where(c => c.ParentCategoryId == categoryId && c.IsDeleted == false)
                .OrderBy(c => c.Order)
                .ToListAsync(cancellation);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellation)
        {
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task CreateSkillCategoryAsync(Category skillCategory, CancellationToken cancellation)
        {
            _context.SkillCategories.Add(skillCategory);
        }

        public async Task DeleteSkillCategoryAsync(int id, CancellationToken cancellation)
        {
            await EditDelete(id, true, cancellation);
        }

        public async Task RetrieveSkillCategoryAsync(int id, CancellationToken cancellation)
        {
            await EditDelete(id, false, cancellation);
        }

        private async Task EditDelete(int parentId, bool delete, CancellationToken cancellation)
        {
            var parent = await _context.SkillCategories
                .Where(c => c.Id == parentId)
                .Include(c => c.ChildCategories)
                .FirstAsync(cancellation);

            foreach (var child in parent.ChildCategories) 
                await EditDelete(child.Id, delete, cancellation);

            parent.IsDeleted = delete;
        }
    }
}
