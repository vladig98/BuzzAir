namespace BuzzAir.Areas.Admin.Services
{
    public class FlightsService(
        IFlightsRepository flightsRepository,
        ICachingService cachingService) : IFlightsService
    {
        public async Task<PaginatedList<FlightDTO>> AllAsync(int? pageNumber, CancellationToken token)
        {
            long count = await flightsRepository.GetCountAsync(token);
            List<Flight> flights = await GetAllFlightsAsync(pageNumber, GlobalConstants.ItemsPerPage, token);
            PaginatedList<FlightDTO> paginatedList = FlightFactory.GetPaginatedList(pageNumber ?? 0, count, flights);

            return paginatedList;
        }

        public async Task<PaginatedList<FlightDTO>> AllDeletedAsync(int? pageNumber, CancellationToken token)
        {
            long count = await flightsRepository.GetDeletedCountAsync(token);
            List<Flight> flights = await GetAllFlightsAsync(pageNumber, GlobalConstants.ItemsPerPage, deleted: true, token);
            PaginatedList<FlightDTO> paginatedList = FlightFactory.GetPaginatedList(pageNumber ?? 0, count, flights);

            return paginatedList;
        }

        public async Task CreateAsync(CreateFlightVM model, CancellationToken token)
        {
            //ArgumentNullException.ThrowIfNull(model);
            //Flight

            //Flight flight = FlightFactory.Create(model);
            //await flightsRepository.CreateAsync(flight, token);
        }

        public Task DeleteAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(EditFlightVM model, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteFlightVM> GetDeleteDetailsAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<EditFlightVM> GetEditDetailsAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<RestoreFlightVM> GetRestoreDetailsAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private Task<List<Flight>> GetAllFlightsAsync(int? pageNumber = null, int? itemPerPage = null, CancellationToken token = default) =>
            GetAllFlightsAsync(pageNumber, itemPerPage, deleted: false, token);

        private Task<List<Flight>> GetAllFlightsAsync(int? pageNumber = null, int? itemPerPage = null, bool deleted = false, CancellationToken token = default)
        {
            async Task<List<Flight>> dbFunc(CancellationToken ct) { return await flightsRepository.AllAsync(pageNumber, itemPerPage, ct); }
            async Task<List<Flight>> dbFuncDeleted(CancellationToken ct) { return await flightsRepository.AllDeletedAsync(pageNumber, itemPerPage, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(GlobalConstants.FLIGHTS_DELETED_CACHE_KEY, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(GlobalConstants.FLIGHTS_CACHE_KEY, dbFunc, token);
        }

        private Task<Flight> GetFlightByIdAsync(string id, CancellationToken token = default) =>
            GetFlightByIdAsync(id, deleted: false, token);

        private Task<Flight> GetFlightByIdAsync(string id, bool deleted = false, CancellationToken token = default)
        {
            string cacheKey = string.Format(GlobalConstants.FLIGHT_CACHE_KEY, id);
            async Task<Flight> dbFunc(CancellationToken ct) { return await flightsRepository.GetByIdAsync(id, ct); }
            async Task<Flight> dbFuncDeleted(CancellationToken ct) { return await flightsRepository.GetDeletedByIdAsync(id, ct); }

            if (deleted)
            {
                return cachingService.GetAsync(cacheKey, dbFuncDeleted, token);
            }

            return cachingService.GetAsync(cacheKey, dbFunc, token);
        }

        private static void VerifyId(string id) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(id);

        public Task<List<SelectListItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId)
        {
            throw new NotImplementedException();
        }

        public Task<Flight?> GetById(string v)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure)
        {
            throw new NotImplementedException();
        }

        public List<FlightViewModel> GetFlightsDetails(ICollection<BookingFlight> flights)
        {
            throw new NotImplementedException();
        }

        public List<FlightViewModel> GetViewModels(IEnumerable<Flight> outboundFlights)
        {
            throw new NotImplementedException();
        }
    }
}
