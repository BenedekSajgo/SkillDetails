using DAL.Entities;

namespace DAL.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        #region Person
        Task<bool> SaveChangesAsync(CancellationToken cancellation);
        Task<bool> PersonByNameAndMailExistsAsync(string lastName, string firstName, string mail, CancellationToken cancellation);
        Task<bool> PersonByIdExistsAsync(int id, CancellationToken cancellation);
        Task<Person?> GetPersonAsync(int id, CancellationToken cancellation);
        Task<IEnumerable<Person>> GetPersonsAsync(bool includeHasBeenDeleted, CancellationToken cancellation);
        void CreatePersonAsync(Person person);
        Task DeletePerson(int id, CancellationToken cancellation);
        Task RetrievePerson(int id, CancellationToken cancellation);
        #endregion

        #region Education
        Task<Education?> GetEducationAsync(int id, CancellationToken cancellation);
        Task<IEnumerable<Education>> GetDeletedEducationsForPerson(int personId, CancellationToken cancellation);
        Task CreateEducationAsync(int personId, Education education, CancellationToken cancellation);
        void DeleteEducation(int id);
        void RetrieveEduation(int id);
        #endregion
    }
}
