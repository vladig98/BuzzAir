using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;

namespace BuzzAir.Services.Contracts
{
    public interface IServiceService
    {
        Task<IService> Create(string name);
        Task<IService> Create(BaggageType baggageType);
    }
}
