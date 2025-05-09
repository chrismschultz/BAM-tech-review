using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonAstronaut?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<PersonAstronaut>> GetAllAsync(CancellationToken cancellationToken);
        Task<Person> UpdateAsync(string currentName, string newName, CancellationToken cancellationToken);
        Task<int> CreateAsync(string name, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
        Person? GetByCurrentName(string name, CancellationToken cancellationToken);
    }
}
