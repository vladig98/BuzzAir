using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services.Contracts
{
    public interface IPassengerServiceService
    {
        Task<PersonService> Create(IPassenger passenger, IService service);
    }
}
