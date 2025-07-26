namespace BuzzAir.Areas.Admin.Services
{
    public class StateService(
        IStateRepository stateRepository,
        ICachingService cachingService) : IStateService
    {
        public async Task<PaginatedList<StateDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await stateRepository.GetCountAsync(token);

            List<State> states = await GetAllStatesAsync(pageNumber, GlobalConstants.ItemsPerPage, includeCountry: true, token);
            PaginatedList<StateDTO> paginatedList = StateFactory.GetPaginatedList(pageNumber ?? 0, count, states);

            return paginatedList;
        }

        public async Task<PaginatedList<StateDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await stateRepository.GetDeletedCountAsync(token);

            List<State> states = await GetAllStatesAsync(pageNumber, GlobalConstants.ItemsPerPage, includeCountry: true, deleted: true, token);
            PaginatedList<StateDTO> paginatedList = StateFactory.GetPaginatedList(pageNumber ?? 0, count, states);

            return paginatedList;
        }

        public async Task CreateAsync(CreateStateVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.Name);

            State state = StateFactory.Create(model);
            await stateRepository.CreateAsync(state, token);
        }

        public async Task DeleteAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await stateRepository.DeleteAsync(id, includeCountry: false, token);
        }

        public async Task EditAsync(EditStateVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyStringValue(model.Name);
            VerifyStringValue(model.CountryId);
            VerifyStringValue(model.Id);

            State state = await GetStateByIdAsync(model.Id, includeCountry: false, token);
            bool canChangeLocation = await stateRepository.CanChangeLocationAsync(model.Id, token);

            StateFactory.Update(state, model, canChangeLocation);
            await stateRepository.EditAsync(state, token);
        }

        public async Task<State?> GetByIdAsync(string id, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return await GetStateByIdAsync(id, includeCountry: true, token);
        }

        public async Task<DeleteStateVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            State state = await GetStateByIdAsync(id, includeCountry: true, token);
            DeleteStateVM model = StateFactory.GetDeleteViewModel(state);

            return model;
        }

        public async Task<EditStateVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            State state = await GetStateByIdAsync(id, includeCountry: true, token);
            EditStateVM model = StateFactory.GetEditViewModel(state);

            return model;
        }

        public async Task<RestoreStateVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);

            State state = await GetStateByIdAsync(id, includeCountry: true, deleted: true, token);
            RestoreStateVM model = StateFactory.GetRestoreViewModel(state);

            return model;
        }

        public async Task<List<SelectListItem>> GetStatesForSelectAsync(CancellationToken token)
        {
            List<State> states = await GetAllStatesAsync(includeCountry: true, deleted: false, token: token);
            List<SelectListItem> statesSelect = StateFactory.GetStatesAsSelectItems(states);

            return statesSelect;
        }

        public async Task RestoreAsync(string id, CancellationToken token)
        {
            VerifyStringValue(id);
            await stateRepository.RestoreAsync(id, includeCountry: false, token);
        }

        private Task<List<State>> GetAllStatesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            bool includeCountry = false,
            CancellationToken token = default) =>
            GetAllStatesAsync(pageNumber, itemPerPage, includeCountry, deleted: false, token);

        private Task<List<State>> GetAllStatesAsync(
            int? pageNumber = null,
            int? itemPerPage = null,
            bool includeCountry = false,
            bool deleted = false,
            CancellationToken token = default)
        {
            async Task<List<State>> dbFunc(CancellationToken ct) { return await stateRepository.AllAsync(pageNumber, itemPerPage, includeCountry, ct); }
            async Task<List<State>> dbFuncDeleted(CancellationToken ct) { return await stateRepository.AllDeletedAsync(pageNumber, itemPerPage, includeCountry, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.STATES_DELETED_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.STATES_CACHE_KEY, dbFunc, token);
        }

        private Task<State> GetStateByIdAsync(string id, bool includeCountry = false, CancellationToken token = default) =>
            GetStateByIdAsync(id, includeCountry, deleted: false, token);

        private Task<State> GetStateByIdAsync(string id, bool includeCountry = false, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.STATE_CACHE_KEY, id);
            async Task<State> dbFunc(CancellationToken ct) { return await stateRepository.GetByIdAsync(id, includeCountry, ct); }
            async Task<State> dbFuncDeleted(CancellationToken ct) { return await stateRepository.GetDeletedByIdAsync(id, includeCountry, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(cacheKey, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(cacheKey, dbFunc, token);
        }

        private static void VerifyStringValue(string value) =>
            string.IsNullOrWhiteSpace(value);
    }
}
