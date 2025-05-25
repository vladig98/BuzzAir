namespace BuzzAir.Factories
{
    public static class AircraftFactory
    {
        public static AircraftEditViewModel CreateEditViewModel(Aircraft aircraft)
        {
            AircraftEditViewModel viewModel = new()
            {
                Id = aircraft.Id,
                Name = aircraft.Name,
                NumbeOfSeats = aircraft.NumberOfSeats
            };

            return viewModel;
        }

        public static Aircraft CreateAircraft(string name, int numberOfSeats)
        {
            Aircraft aircraft = new()
            {
                Name = name,
                NumberOfSeats = numberOfSeats
            };

            return aircraft;
        }

        internal static List<SelectListItem> GetAircraftForSelect(List<Aircraft> aircraftList)
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
    }
}
