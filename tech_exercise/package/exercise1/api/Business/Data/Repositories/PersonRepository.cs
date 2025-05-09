
using Microsoft.EntityFrameworkCore;

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

        public Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Person?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Person person, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
