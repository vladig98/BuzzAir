namespace BuzzAir.Areas.Admin.Factories
{
    public static class CityFactory
    {
        public static City Create(CreateCityVM model)
        {
            City city = new()
            {
                Name = model.Name,
                Country = model.Country!,
                State = model.State
            };

            return city;
        }

        public static CityViewModel GetViewModel(City city)
        {
            CityViewModel viewModel = new()
            {
                Name = city.Name
            };

            return viewModel;
        }

        public static CreateCityVM InitializeCreateCityViewModel(List<SelectListItem> countries, List<SelectListItem> states)
        {
            CreateCityVM viewModel = new()
            {
                CountryOptions = countries,
                StatesOptions = states
            };

            return viewModel;
        }

        public static DeleteCityVM GetDeleteViewModel(City city)
        {
            DeleteCityVM viewModel = new()
            {
                Id = city.Id,
                Name = city.Name,
                CountryName = city.Country.Name,
                StateName = city.State?.Name ?? string.Empty
            };

            return viewModel;
        }

        public static EditCityVM GetEditViewModel(City city)
        {
            EditCityVM viewModel = new()
            {
                Id = city.Id,
                Name = city.Name,
                CountryName = city.Country.Name,
                StateName = city.State?.Name ?? string.Empty
            };

            return viewModel;
        }

        public static PaginatedList<CityDTO> GetPaginatedList(int pageNumber, long count, List<City> cities)
        {
            List<CityDTO> dtos = MapModelToDTO(cities);
            PaginatedList<CityDTO> paginatedList = new(dtos, count, pageNumber, GlobalConstants.ItemsPerPage);

            return paginatedList;
        }

        public static RestoreCityVM GetRestoreViewModel(City city)
        {
            RestoreCityVM viewModel = new()
            {
                Id = city.Id,
                Name = city.Name,
                CountryName = city.Country.Name,
                StateName = city.State?.Name ?? string.Empty
            };

            return viewModel;
        }

        public static void Update(City city, EditCityVM model, bool canChangeLocation)
        {
            city.Name = model.Name;

            if (!canChangeLocation)
            {
                return;
            }

            city.State = model.State;
            city.Country = model.Country!;
        }

        public static void UpdateEditViewModelWithSelects(EditCityVM model, List<SelectListItem> countries, List<SelectListItem> states)
        {
            model.Countries = countries;
            model.States = states;
        }

        private static List<CityDTO> MapModelToDTO(List<City> cities)
        {
            List<CityDTO> dtos = new(cities.Count);

            foreach (City city in cities)
            {
                CityDTO dto = new(
                    city.Id,
                    city.Name,
                    city.State?.Name ?? string.Empty,
                    city.Country.Name);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
