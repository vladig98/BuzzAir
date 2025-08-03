namespace BuzzAir.Areas.Admin.Services;

public sealed class StateService(BuzzAirDbContext dbContext) : IStateService
{
    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.States.AnyAsync(s => s.Id == id, token);
    }

    public Task<State?> GetStateByIdAsync(string? stateId, CancellationToken token)
    {
        return dbContext.States.FirstOrDefaultAsync(s => s.Id == stateId, token); ;
    }

    public Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default)
    {
        return dbContext.States
            .Where(s => s.CountryId == countryId)
            .AsNoTracking()
            .Select(s => new StateDTO(s.Id, s.Name))
            .ToListAsync(token);
    }
}