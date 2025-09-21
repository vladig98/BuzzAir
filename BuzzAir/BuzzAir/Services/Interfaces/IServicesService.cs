namespace BuzzAir.Services.Interfaces;

public interface IServicesService
{
    Task<List<ServiceDto>> GetServicesAsync(CancellationToken token);
    Task<List<ServiceDto>> GetSeatServicesAsync(CancellationToken token);
    Task<List<ServiceDto>> GetBaggageServicesAsync(CancellationToken token);
    Task<Service?> GetServiceModelByIdAsync(string id, CancellationToken token);
}
