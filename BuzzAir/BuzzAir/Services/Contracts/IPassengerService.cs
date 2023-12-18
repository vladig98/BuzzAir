using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;

namespace BuzzAir.Services.Contracts
{
    public interface IPassengerService
    {
        Task<IPassenger> Create(string firstName, string lastName, Gender gender, BaggageType baggageType, ICollection<IService> services);
    }
}
