using DAL.Data;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly CVContext _context;

        public SkillRepository(CVContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Skill
        public async Task<bool> SaveChangesAsync(CancellationToken cancellation)
        {
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<bool> SkillExistsAsync(string skillName, CancellationToken cancellation)
        {
            return await _context.Skills.AnyAsync(s => s.Name == skillName, cancellation);
        }

        public async Task<Skill?> GetSkillAsync(int id, CancellationToken cancellation)
        {
            return await _context.Skills
                .Where(s => s.Id == id && s.IsDeleted == false)
                .Include(s => s.SkillCategory)
                .Include(s => s.SkillLevelList.Where(s => s.IsDeleted == false).OrderBy(s => s.Order))
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync(CancellationToken cancellation)
        {
            return await _context.Skills
                .Where(s => s.IsDeleted == false)
                .Include(s => s.SkillCategory)
                .Include(s => s.SkillLevelList.Where(s => s.IsDeleted == false).OrderBy(s => s.Order))
                .ToListAsync(cancellation);
        }
        
        public async Task<IEnumerable<Skill>> GetDeletedSkillsForPersonAsync(int personId, CancellationToken cancellation)
        {
            return await _context.People
                .Where(p => p.Id == personId)
                .Include(p => p.Skills)
                .SelectMany(p => p.Skills.Where(s => s.IsDeleted == true))
                .Include(s => s.SkillCategory)
                //.Include(s => s.SkillLevelList.Where(s => s.IsDeleted == false).OrderBy(s => s.Order))
                .OrderBy(s => s.SkillCategory.Order)
                .ToListAsync(cancellation);
        }

        public async Task CreateSkillAsync(Skill skill, CancellationToken cancellation)
        {
            var skillsToReorder = await _context.Skills
                .Where(s => s.SkillCategory == skill.SkillCategory)
                .Where(s => s.Order >= skill.Order)
                .ToListAsync(cancellation);

            skillsToReorder.ForEach(s => s.Order++);
            _context.Skills.Add(skill);
        }

        public async Task DeleteSkillAsync(int id, CancellationToken cancellation)
        {
            var skill = await _context.Skills
                .Where(s => s.Id == id)
                .Include(s => s.SkillLevelList)
                .FirstAsync(cancellation);

            foreach(var s in skill.SkillLevelList)
                s.IsDeleted = true;
            skill.IsDeleted = true;
        }

        public async Task RetrieveSkillAsync(int id, CancellationToken cancellation)
        {
            var skill = await _context.Skills
                .Where(s => s.Id == id)
                .Include(s => s.SkillLevelList)
                .FirstAsync(cancellation);

            foreach (var s in skill.SkillLevelList)
                s.IsDeleted = false;
            skill.IsDeleted = false;
        }
        #endregion


        #region SkillLevel
        public async Task<SkillLevel?> GetSkillLevelAsync(int id, CancellationToken cancellation)
        {
            return await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false, cancellation);
        }

        public async Task<IEnumerable<SkillLevel>> GetSkillLevelsForSkillAsync(int skillId, bool includeHasBeenDeleted, CancellationToken cancellation)
        {
            if (includeHasBeenDeleted)
                return await _context.Skills
                    .Where(p => p.Id == skillId)
                    .Include(s => s.SkillLevelList)
                    .SelectMany(s => s.SkillLevelList.OrderBy(s => s.Order))
                    .ToListAsync(cancellation);
            
            return await _context.Skills
                .Where(p => p.Id == skillId)
                .Include(s => s.SkillLevelList)
                .SelectMany(s => s.SkillLevelList.Where(s => s.IsDeleted == false)).OrderBy(s => s.Order)
                .ToListAsync(cancellation);
        }

        public async Task CreateSkillLevelAsync(int skillId, SkillLevel skillLevel, CancellationToken cancellation)
        {
            await  UpdateSkillLevelOrderAsync(skillId, skillLevel, cancellation);

            var skill = await _context.Skills.FirstAsync(s => s.Id == skillId, cancellation);
            skill.SkillLevelList.Add(skillLevel);
        }

        public async Task UpdateSkillLevelOrderAsync(int skillId, SkillLevel skillLevel, CancellationToken cancellation) //in business
        {
            var skillLevelsToReorder = await _context.Skills
                .Where(s => s.Id == skillId)
                .Include(s => s.SkillLevelList)
                .SelectMany(s => s.SkillLevelList)
                .Where(s => s.Order >= skillLevel.Order)
                .ToListAsync(cancellation);

            skillLevelsToReorder.ForEach(s => s.Order++);
        }

        public async Task ResetSkillLevelOrderAsync(int skillId, CancellationToken cancellation)
        {
            var skillLevels = await _context.Skills
                .Where(s => s.Id == skillId)
                .Include(s => s.SkillLevelList)
                .SelectMany(s => s.SkillLevelList)
                .OrderBy(s => s.Order)  
                .ToListAsync(cancellation);

            for(int i = 0; i < skillLevels.Count; i++)
            {
                skillLevels[i].Order = i+1;
            }
        }

        public void DeleteSkillLevel(int id)
        {
            _context.SkillLevels.First(s => s.Id == id).IsDeleted = true;
        }

        public void RetrieveSkillLevel(int id)
        {
            _context.SkillLevels.First(s => s.Id == id).IsDeleted = false;
        }
        #endregion
    }
}
