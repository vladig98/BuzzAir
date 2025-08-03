namespace BuzzAir.Areas.Admin.Services.Interfaces;

public interface ICityService
{
    Task AddCityAsync(CreateCityVM model, CancellationToken token = default);
}
