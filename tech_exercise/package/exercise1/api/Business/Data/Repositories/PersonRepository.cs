
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly StargateContext _context;

        public PersonRepository(StargateContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string name, CancellationToken cancellationToken)
        {
            var person = new Person { Name = name };
            await _context.People.AddAsync(person, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return person.Id;
        }

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.People.AnyAsync(x => x.Name == name, cancellationToken);
        }

        public async Task<IEnumerable<PersonAstronaut>> GetAllAsync(CancellationToken cancellationToken)
        {
            var people = await _context.People.AsNoTracking().Select(x => new PersonAstronaut
            {
                PersonId = x.Id,
                Name = x.Name,
                CurrentRank = x.AstronautDetail != null ? x.AstronautDetail.CurrentRank : "",
                CurrentDutyTitle = x.AstronautDetail != null ? x.AstronautDetail.CurrentDutyTitle : "",
                CareerStartDate = x.AstronautDetail != null ? x.AstronautDetail.CareerStartDate : null,
                CareerEndDate = x.AstronautDetail != null ? x.AstronautDetail.CareerEndDate : null
            }).ToListAsync(cancellationToken);
            return people;
        }

        public async Task<PersonAstronaut?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var person = await _context.People.AsNoTracking().Where(x => x.Name == name)
                .Select(x => new PersonAstronaut
                {
                    PersonId = x.Id,
                    Name = x.Name,
                    CurrentRank = x.AstronautDetail != null ? x.AstronautDetail.CurrentRank : "",
                    CurrentDutyTitle = x.AstronautDetail != null ? x.AstronautDetail.CurrentDutyTitle : "",
                    CareerStartDate = x.AstronautDetail != null ? x.AstronautDetail.CareerStartDate : null,
                    CareerEndDate = x.AstronautDetail != null ? x.AstronautDetail.CareerEndDate : null
                }).FirstOrDefaultAsync();
            return person;
        }

        public Person? GetByCurrentName(string name, CancellationToken cancellationToken)
        {
            var person = _context.People.AsNoTracking().FirstOrDefault(x => x.Name == name);
            return person;
        }

        public async Task<Person> UpdateAsync(string currentName, string newName, CancellationToken cancellationToken)
        {
            var person = await _context.People.FirstOrDefaultAsync(x => x.Name == currentName);
            if (person is null)
            {
                throw new Exception($"ERROR: Person with {currentName} does not exist after pre processing check");
            }
            person.Name = newName;
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }
    }
}
