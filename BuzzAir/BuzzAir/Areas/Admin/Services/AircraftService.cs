namespace BuzzAir.Areas.Admin.Services
{
    public class AircraftService(IAircraftRepository aircraftRepository) : IAircraftService
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

            Aircraft aircraft = await aircraftRepository.GetByIdAsync(model.Id, token);
            bool canChangeSeats = await aircraftRepository.CanChangeSeatsAsync(aircraft.Id, model.Seats, token);

            AircraftFactory.Update(aircraft, model, canChangeSeats);
            await aircraftRepository.EditAsync(aircraft, token);
        }

        public async Task<EditAircraftVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            VerifyId(id);

            Aircraft aircraft = await aircraftRepository.GetByIdAsync(id, token);
            EditAircraftVM viewModel = AircraftFactory.GetEditViewModel(aircraft);

            return viewModel;
        }

        public async Task<DeleteAircraftVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            VerifyId(id);

            Aircraft aircraft = await aircraftRepository.GetByIdAsync(id, token);
            DeleteAircraftVM viewModel = AircraftFactory.GetDeleteViewModel(aircraft);

            return viewModel;
        }

        public async Task<RestoreAircraftVM> GetRestoreDetailsAsync(string aircraftId, CancellationToken token)
        {
            VerifyId(aircraftId);

            Aircraft aircraft = await aircraftRepository.GetDeletedByIdAsync(aircraftId, token);
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
            List<Aircraft> aircraft = await aircraftRepository.AllAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<AircraftDTO> paginatedList = AircraftFactory.GetPaginatedList(pageNumber ?? 0, count, aircraft);

            return paginatedList;
        }

        public async Task<PaginatedList<AircraftDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await aircraftRepository.GetDeletedCountAsync(token);
            List<Aircraft> aircraft = await aircraftRepository.AllDeletedAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<AircraftDTO> paginatedList = AircraftFactory.GetPaginatedList(pageNumber ?? 0, count, aircraft);

            return paginatedList;
        }

        public async Task<List<SelectListItem>> GetAircraftForSelect(CancellationToken token)
        {
            List<Aircraft> aircraft = await aircraftRepository.AllAsync(token: token);
            List<SelectListItem> select = AircraftFactory.GetAircraftForSelect(aircraft);

            return select;
        }

        public async Task<Aircraft> GetByIdAsync(string id, CancellationToken token)
        {
            VerifyId(id);
            Aircraft aircraft = await aircraftRepository.GetByIdAsync(id, token);

            return aircraft;
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
