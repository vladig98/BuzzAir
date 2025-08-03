namespace BuzzAir.Areas.Admin.Services;

public sealed class StateService(
    BuzzAirDbContext dbContext,
    ICountryService countryService) : IStateService
{
    public async Task AddStateAsync(CreateStateVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        Country country = await countryService.GetCountryByIdAsync(viewModel.CountryId, token);

        State state = new()
        {
            Name = viewModel.Name,
            Country = country
        };

        _ = await dbContext.States.AddAsync(state, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task<int> CountAsync(CancellationToken token = default)
    {
        return dbContext.States.Where(s => !s.IsDeleted).CountAsync(token);
    }

    public Task<int> CountDeletedAsync(CancellationToken token = default)
    {
        return dbContext.States.Where(s => s.IsDeleted).CountAsync(token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.States.Where(s => s.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return dbContext.States.AnyAsync(s => s.Id == id, token);
    }

    public async Task<List<StateDTO>> GetAllDeletedStatesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.States.CountAsync(s => s.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<State> states = await dbContext.States.Where(s => s.IsDeleted)
                                                   .Include(s => s.Country)
                                                   .OrderBy(s => s.Name)
                                                   .Skip((pageNumber - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .AsNoTracking()
                                                   .ToListAsync(token);

        List<StateDTO> dtos = [.. states.Select(s => new StateDTO(s.Id, s.Name, s.Country.Name))];

        return dtos;
    }

    public async Task<List<StateDTO>> GetAllStatesAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.States.CountAsync(s => !s.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<State> states = await dbContext.States.Where(s => !s.IsDeleted)
                                                   .Include(s => s.Country)
                                                   .OrderBy(s => s.Name)
                                                   .Skip((pageNumber - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .AsNoTracking()
                                                   .ToListAsync(token);

        List<StateDTO> dtos = [.. states.Select(s => new StateDTO(s.Id, s.Name, s.Country.Name))];

        return dtos;
    }

    public async Task<StateDTO> GetDeletedStateByIdAsync(string id, CancellationToken token = default)
    {
        State state = await dbContext.States.Where(s => s.IsDeleted)
                                            .Include(s => s.Country)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(s => s.Id == id, token)
            ?? throw new KeyNotFoundException($"Can't find a state with id {id}.");

        StateDTO dto = new(state.Id, state.Name, state.Country.Name);

        return dto;
    }

    public async Task<StateDTO> GetStateByIdAsync(string stateId, CancellationToken token = default)
    {
        State state = await dbContext.States.Where(s => !s.IsDeleted)
                                            .Include(s => s.Country)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(s => s.Id == stateId, token)
            ?? throw new KeyNotFoundException($"Can't find a state with id {stateId}.");

        StateDTO dto = new(state.Id, state.Name, state.Country.Name);

        return dto;
    }

    public Task<State?> GetStateModelByIdAsync(string? stateId, CancellationToken token = default)
    {
        return dbContext.States.FirstOrDefaultAsync(s => s.Id == stateId, token);
    }

    public Task<List<StateDTO>> GetStatesByCountryAsync(string countryId, CancellationToken token = default)
    {
        return dbContext.States
            .Include(s => s.Country)
            .Where(s => s.CountryId == countryId)
            .AsNoTracking()
            .Select(s => new StateDTO(s.Id, s.Name, s.Country.Name))
            .ToListAsync(token);
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.States.Where(s => s.Id == id).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.States.Where(s => s.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateStateAsync(EditStateVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.States.Where(s => s.Id == viewModel.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    s => s.Name,
                    s => viewModel.Name)
                .SetProperty(
                    s => s.CountryId,
                    s => viewModel.CountryId), token);
    }
}