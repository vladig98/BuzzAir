namespace BuzzAir.Areas.Admin.Services
{
    public class AircraftService(
        IAircraftRepository aircraftRepository,
        ICachingService cachingService) : IAircraftService
    {
        public async Task CreateAsync(CreateAircraftVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifyName(model.Name);
            VerifySeats(model.Seats);

            Aircraft aircraft = AircraftFactory.Create(model);
            await aircraftRepository.CreateAsync(aircraft, token);
        }

        public async Task EditAsync(EditAircraftVM model, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(model);
            VerifySeats(model.Seats);
            VerifyName(model.Name);
            VerifyId(model.Id);

            Aircraft aircraft = await GetAircraftByIdAsync(model.Id, token);
            bool canChangeSeats = await aircraftRepository.CanChangeSeatsAsync(aircraft!.Id, model.Seats, token);

            AircraftFactory.Update(aircraft, model, canChangeSeats);
            await aircraftRepository.EditAsync(aircraft, token);
        }

        public async Task<EditAircraftVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyId(id);

            Aircraft aircraft = await GetAircraftByIdAsync(id, token);
            EditAircraftVM viewModel = AircraftFactory.GetEditViewModel(aircraft);

            return viewModel;
        }

        public async Task<DeleteAircraftVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyId(id);

            Aircraft aircraft = await GetAircraftByIdAsync(id, token);
            DeleteAircraftVM viewModel = AircraftFactory.GetDeleteViewModel(aircraft);

            return viewModel;
        }

        public async Task<RestoreAircraftVM> GetRestoreDetailsAsync(string aircraftId, CancellationToken token)
        {
            VerifyId(aircraftId);

            Aircraft aircraft = await GetAircraftByIdAsync(aircraftId, deleted: true, token);
            RestoreAircraftVM viewModel = AircraftFactory.GetRestoreViewModel(aircraft);

            return viewModel;
        }

        public Task DeleteAsync(string id, CancellationToken token) =>
            aircraftRepository.DeleteAsync(id, token);

        public Task RestoreAsync(string id, CancellationToken token) =>
            aircraftRepository.RestoreAsync(id, token);

        public async Task<PaginatedList<AircraftDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await aircraftRepository.GetCountAsync(token);
            List<Aircraft> aircraft = await GetAllAircraftAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<AircraftDTO> paginatedList = AircraftFactory.GetPaginatedList(pageNumber ?? 0, count, aircraft);

            return paginatedList;
        }

        public async Task<PaginatedList<AircraftDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await aircraftRepository.GetDeletedCountAsync(token);
            List<Aircraft> aircraft = await GetAllAircraftAsync(deleted: true, token: token);
            PaginatedList<AircraftDTO> paginatedList = AircraftFactory.GetPaginatedList(pageNumber ?? 0, count, aircraft);

            return paginatedList;
        }

        public async Task<List<SelectListItem>> GetAircraftForSelect(CancellationToken token)
        {
            List<Aircraft> aircraft = await GetAllAircraftAsync(deleted: false, token: token);
            List<SelectListItem> select = AircraftFactory.GetAircraftForSelect(aircraft);

            return select;
        }

        public async Task<Aircraft> GetByIdAsync(string id, CancellationToken token)
        {
            VerifyId(id);
            Aircraft aircraft = await GetAircraftByIdAsync(id, token);

            return aircraft;
        }

        private Task<List<Aircraft>> GetAllAircraftAsync(int? pageNumber = null, int? itemPerPage = null, CancellationToken token = default) =>
            GetAllAircraftAsync(pageNumber, itemPerPage, deleted: false, token);

        private Task<List<Aircraft>> GetAllAircraftAsync(int? pageNumber = null, int? itemPerPage = null, bool deleted = false, CancellationToken token = default)
        {
            async Task<List<Aircraft>> dbFunc(CancellationToken ct) { return await aircraftRepository.AllAsync(pageNumber, itemPerPage, ct); }
            async Task<List<Aircraft>> dbFuncDeleted(CancellationToken ct) { return await aircraftRepository.AllDeletedAsync(pageNumber, itemPerPage, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.AIRCRAFT_DELETED_ALL_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.AIRCRAFT_ALL_CACHE_KEY, dbFunc, token);
        }

        private Task<Aircraft> GetAircraftByIdAsync(string id, CancellationToken token = default) => 
            GetAircraftByIdAsync(id, deleted: false, token);

        private Task<Aircraft> GetAircraftByIdAsync(string id, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.AIRCRAFT_CACHE_KEY, id);
            async Task<Aircraft> dbFunc(CancellationToken ct) { return await aircraftRepository.GetByIdAsync(id, ct); }
            async Task<Aircraft> dbFuncDeleted(CancellationToken ct) { return await aircraftRepository.GetDeletedByIdAsync(id, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(cacheKey, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(cacheKey, dbFunc, token);
        }

        private static void VerifySeats(int seats)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(seats, GlobalConstants.MinimumNumberOfSeatsForAnAircraft);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(seats, GlobalConstants.MaximumNumberOfSeatsForAnAircraft);
        }

        private static void VerifyName(string name) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

        private static void VerifyId(string id) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(id);
    }
}
