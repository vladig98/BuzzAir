namespace BuzzAir.Services
{
    public class AircraftService(BuzzAirDbContext context) : IAircraftService
    {
        public async Task DeleteAsync(string id)
        {
            Aircraft aircraft = await GetByIdAsync(id);
            aircraft.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task EditAsync(string id, string name, int numberOfSeats)
        {
            Task<Aircraft> aircraftTask = GetByIdAsync(id);
            Task<bool> canChangeSeatsTask = CanChangeSeatsAsync(id, numberOfSeats);

            await Task.WhenAll(aircraftTask, canChangeSeatsTask);

            if (!canChangeSeatsTask.Result)
            {
                throw new ArgumentException(
                    $"There are already flights with more passengers than the aircraft allows. Invalid number of seats: {numberOfSeats}");
            }

            Aircraft aircraft = aircraftTask.Result;

            aircraft.Name = name;
            aircraft.NumberOfSeats = numberOfSeats;

            await context.SaveChangesAsync();
        }

        public async Task CreateAsync(string name, int numberOFSeats)
        {
            Aircraft aircraft = AircraftFactory.CreateAircraft(name, numberOFSeats);

            await context.Aircrafts.AddAsync(aircraft);
            await context.SaveChangesAsync();
        }

        public async Task<PaginatedList<Aircraft>> GetAllAsPaginatedListAsync(int pageSize, int? pageNumber)
        {
            pageNumber ??= 1;
            int page = pageNumber.Value;

            Task<IEnumerable<Aircraft>> aircraftTask = GetAllAsync(pageSize, page);
            Task<int> countTask = GetCountAsync();

            await Task.WhenAll(aircraftTask, countTask);
            PaginatedList<Aircraft> paginatedItems = new(aircraftTask.Result, countTask.Result, page, pageSize);

            return paginatedItems;
        }

        public async Task<AircraftEditViewModel> GetEditModelAsync(string aircraftId)
        {
            Aircraft aircraft = await GetByIdAsync(aircraftId);
            AircraftEditViewModel model = AircraftFactory.CreateEditViewModel(aircraft);

            return model;
        }

        private async Task<bool> CanChangeSeatsAsync(string id, int numberOfSeats)
        {
            // Check if there is already a flight booked on that aircraft
            // The verify if the new number of seats is enough to cover these flights
            return !await context.Flights
                .Include(x => x.Passengers)
                .Include(x => x.Aircraft)
                .Where(x => x.Aircraft.Id == id)
                .AnyAsync(x => x.Passengers.Count > numberOfSeats);
        }

        private async Task<IEnumerable<Aircraft>> GetAllAsync(int pageSize, int pageNumber)
        {
            int toSkip = (pageNumber - 1) * pageSize;

            return await context.Aircrafts
                .Where(x => !x.IsDeleted)
                .Skip(toSkip)
                .Take(pageSize)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Aircraft> GetByIdAsync(string id)
        {
            Aircraft aircraft = await context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException($"Aircraft with {id} does not exist.");

            return aircraft;
        }

        private async Task<int> GetCountAsync()
        {
            return await context.Aircrafts.Where(x => !x.IsDeleted).CountAsync();
        }

        public async Task<List<SelectListItem>> GetAircraftForSelect()
        {
            List<Aircraft> aircraft = await context.Aircrafts
                .Where(x => !x.IsDeleted)
                .AsNoTracking()
                .ToListAsync();

            List<SelectListItem> selectItems = AircraftFactory.GetAircraftForSelect(aircraft);

            return selectItems;
        }
    }
}
