namespace StargateAPI.Business.Data.Repositories
{
    public interface IAstronautDutyRepository
    {
        Task CreateAsync(AstronautDuty duty, CancellationToken ct);
        Task<List<AstronautDuty>> GetDutiesByNameAsync(int personId, CancellationToken ct);
    }
}
