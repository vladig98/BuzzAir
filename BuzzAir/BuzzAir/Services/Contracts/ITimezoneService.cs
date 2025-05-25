namespace BuzzAir.Services.Contracts
{
    public interface ITimezoneService
    {
        Task<IEnumerable<Timezone>> GetAllAsync();
    }
}
