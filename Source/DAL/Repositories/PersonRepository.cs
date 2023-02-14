using DAL.Data;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly CVContext _context;

        public PersonRepository(CVContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        #region Person
        public async Task<bool> PersonByIdExistsAsync(int id, CancellationToken cancellation)
        {
            return await _context.People.AnyAsync(p => p.Id == id && p.IsDeleted == false, cancellation);
        }

        public async Task<bool> PersonByNameAndMailExistsAsync(string lastName, string firstName, string mail, CancellationToken cancellation)
        {
            return await _context.People.AnyAsync(
                p => p.LastName == lastName && 
                p.FirstName == firstName && 
                p.Email == mail &&
                p.IsDeleted == false, cancellation);
        }

        public async Task<Person?> GetPersonAsync(int id, CancellationToken cancellation)
        {
            return await _context.People
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Include(p => p.Skills).Where(p => p.IsDeleted == false)
                .Include(p => p.Projects.Where(p => p.IsDeleted == false))
                .Include(p => p.Educations.Where(e => e.IsDeleted == false))
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync(bool includeHasBeenDeleted, CancellationToken cancellationToken)
        {
            if (includeHasBeenDeleted) 
                return await _context.People
                    .OrderByDescending(p => p.IsDeleted)
                    .ThenBy(p => p.LastName)
                    .ToListAsync(cancellationToken);

            return await _context.People
                .Where(p => p.IsDeleted == false)
                .OrderBy(p => p.LastName)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellation)
        {
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public void CreatePersonAsync(Person person)
        {
            _context.People.Add(person);
        }

        public async Task DeletePerson(int id, CancellationToken cancellation)
        {
            await EditIsDeleted(id, true, cancellation);
        }

        public async Task RetrievePerson(int id, CancellationToken cancellation)
        {
            await EditIsDeleted(id, false, cancellation);
        }

        private async Task EditIsDeleted(int id, bool isDeletedState, CancellationToken cancellation)
        {
            var person = await _context.People
                                .Where(p => p.Id == id)
                                .Include(p => p.Educations)
                                .FirstAsync(cancellation);

            foreach (var e in person.Educations)
                e.IsDeleted = isDeletedState;
            person.IsDeleted = isDeletedState;
        }
        #endregion


        #region Education
        public async Task<Education?> GetEducationAsync(int id, CancellationToken cancellation)
        {
            return await _context.Educations.FirstOrDefaultAsync(e => e.Id == id, cancellation);
        }

        public async Task<IEnumerable<Education>> GetDeletedEducationsForPerson(int personId, CancellationToken cancellation)
        {
            return await _context.People
                                .Where(p => p.Id == personId)
                                .Include(p => p.Educations)
                                .SelectMany(p => p.Educations.Where(e => e.IsDeleted == true))
                                .ToListAsync(cancellation);
        }

        public async Task CreateEducationAsync (int personId, Education education, CancellationToken cancellation)
        {
            var person = await _context.People.FirstAsync(s => s.Id == personId, cancellation);
            person.Educations.Add(education);
        }

        public void DeleteEducation(int id)
        {
            _context.Educations.First(e => e.Id == id).IsDeleted = true;
        }

        public void RetrieveEduation(int id)
        {
            _context.Educations.First(e => e.Id == id).IsDeleted = false;
        }
        #endregion
    }
}
