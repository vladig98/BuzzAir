using BuzzAir.Models.DbModels;

namespace BuzzAir.Services.Contracts
{
    public interface ITimezoneService
    {
        Task<IEnumerable<Timezone>> GetAll();
    }
}
