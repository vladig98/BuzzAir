namespace BuzzAir.Areas.Admin.Factories
{
    public static class AircraftFactory
    {
        public static Aircraft Create(CreateAircraftVM model)
        {
            Aircraft aircraft = new()
            {
                Name = model.Name,
                NumberOfSeats = model.Seats
            };

            return aircraft;
        }

        public static EditAircraftVM GetEditViewModel(Aircraft aircraft)
        {
            EditAircraftVM viewModel = new()
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                Seats = aircraft.NumberOfSeats
            };

            return viewModel;
        }

        public static DeleteAircraftVM GetDeleteViewModel(Aircraft aircraft)
        {
            DeleteAircraftVM viewModel = new()
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                Seats = aircraft.NumberOfSeats
            };

            return viewModel;
        }

        public static RestoreAircraftVM GetRestoreViewModel(Aircraft aircraft)
        {
            RestoreAircraftVM viewModel = new()
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                Seats = aircraft.NumberOfSeats
            };

            return viewModel;
        }

        public static PaginatedList<AircraftDTO> GetPaginatedList(int pageNumber, long count, List<Aircraft> aircraft)
        {
            List<AircraftDTO> dtos = MapModelToDTO(aircraft);
            PaginatedList<AircraftDTO> paginatedList = new(dtos, count, pageNumber, GlobalConstants.ItemsPerPage);

            return paginatedList;
        }

        public static void Update(Aircraft aircraft, EditAircraftVM model, bool canChangeSeats)
        {
            int seats = model.Seats;

            if (!canChangeSeats)
            {
                throw new InvalidOperationException($"Can't change the seats of aircraft {aircraft.Name} to {seats}");
            }

            aircraft.Name = model.Name;
            aircraft.NumberOfSeats = seats;
        }

        public static List<SelectListItem> GetAircraftForSelect(List<Aircraft> aircraftList)
        {
            List<SelectListItem> list = [];

            foreach (Aircraft aircraft in aircraftList)
            {
                SelectListItem aircraftItem = new()
                {
                    Text = aircraft.Name,
                    Value = aircraft.Id
                };

                list.Add(aircraftItem);
            }

            return list;
        }

        private static List<AircraftDTO> MapModelToDTO(List<Aircraft> aircraft)
        {
            List<AircraftDTO> dtos = new(aircraft.Count);

            foreach (Aircraft air in aircraft)
            {
                AircraftDTO dto = new(air.Id, air.Name, air.NumberOfSeats);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
