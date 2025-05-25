namespace BuzzAir.Services.Interfaces
{
    public interface ITimezoneService
    {
        Task<IEnumerable<Timezone>> GetAllAsync();
    }
}
