namespace StargateAPI.Business.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Person person, CancellationToken cancellationToken);
        Task<int> CreateAsync(string name, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    }
}
