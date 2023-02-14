using DAL.Entities;

namespace DAL.RepositoryInterfaces
{
    public interface ISkillRepository
    {
        #region Skill
        Task<bool> SaveChangesAsync(CancellationToken cancellation);
        Task<bool> SkillExistsAsync(string skillName, CancellationToken cancellation);
        Task<Skill?> GetSkillAsync(int id, CancellationToken cancellation);
        Task<IEnumerable<Skill>> GetSkillsAsync(CancellationToken cancellation);
        Task<IEnumerable<Skill>> GetDeletedSkillsForPersonAsync(int personId, CancellationToken cancellation);
        Task CreateSkillAsync(Skill skill, CancellationToken cancellation);
        Task DeleteSkillAsync(int id, CancellationToken cancellation);
        Task RetrieveSkillAsync(int id, CancellationToken cancellation);
        #endregion

        #region SkillLevel
        Task<SkillLevel?> GetSkillLevelAsync(int id, CancellationToken cancellation);
        Task<IEnumerable<SkillLevel>> GetSkillLevelsForSkillAsync(int skillId, bool includeHasBeenDeleted, CancellationToken cancellation);
        Task CreateSkillLevelAsync(int SkillId, SkillLevel skillLevel, CancellationToken cancellation);
        Task UpdateSkillLevelOrderAsync(int skillId, SkillLevel skillLevel, CancellationToken cancellation);
        Task ResetSkillLevelOrderAsync(int skillId, CancellationToken cancellation);
        void DeleteSkillLevel(int id);
        void RetrieveSkillLevel(int id);
        #endregion
    }
}
