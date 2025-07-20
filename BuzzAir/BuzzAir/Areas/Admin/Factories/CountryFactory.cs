namespace BuzzAir.Areas.Admin.Factories
{
    public static class CountryFactory
    {
        public static DeleteCountryVM GetDeleteViewModel(Country country)
        {
            DeleteCountryVM model = new()
            {
                Id = country.Id,
                Name = country.Name,
                IsCountry = country.IsCountry,
                ISO = country.ISO
            };

            return model;
        }

        public static EditCountryVM GetEditViewModel(Country country)
        {
            EditCountryVM model = new()
            {
                Id = country.Id,
                Name = country.Name,
                IsCountry = country.IsCountry,
                ISO = country.ISO
            };

            return model;
        }

        public static PaginatedList<CountryDTO> GetPaginatedList(int pageNumber, long count, List<Country> countries)
        {
            List<CountryDTO> dtos = MapModelToDTO(countries);
            PaginatedList<CountryDTO> paginatedList = new(dtos, count, pageNumber, GlobalConstants.ItemsPerPage);

            return paginatedList;
        }

        public static RestoreCountryVM GetRestoreViewModel(Country country)
        {
            RestoreCountryVM model = new()
            {
                Id = country.Id,
                Name = country.Name,
                IsCountry = country.IsCountry,
                ISO = country.ISO
            };

            return model;
        }

        public static void Update(Country country, EditCountryVM model)
        {
            country.Name = model.Name;
            country.IsCountry = model.IsCountry;
            country.ISO = model.ISO;
        }

        public static Country Create(CreateCountryVM model)
        {
            Country country = new()
            {
                Name = model.Name,
                ISO = model.ISO,
                IsCountry = model.IsCountry
            };

            return country;
        }

        public static List<SelectListItem> GetCountriesAsSelectItems(List<Country> countries)
        {
            SelectListGroup countryGroup = new() { Name = "Countries" };
            SelectListGroup dependenciesGroup = new() { Name = "Dependencies and territories not offically recognized as countries" };

            int count = countries.Count;
            List<SelectListItem> countriesSelectItems = new(count);

            for (int i = 0; i < count; i++)
            {
                Country country = countries[i];

                SelectListItem countryListItem = new()
                {
                    Text = country.Name,
                    Value = country.Id,
                    Group = country.IsCountry ? countryGroup : dependenciesGroup
                };

                countriesSelectItems.Add(countryListItem);
            }

            countriesSelectItems = [.. countriesSelectItems.OrderBy(x => x.Group.Name).ThenBy(x => x.Text)];
            return countriesSelectItems;
        }

        private static List<CountryDTO> MapModelToDTO(List<Country> countries)
        {
            List<CountryDTO> dtos = new(countries.Count);

            foreach (Country country in countries)
            {
                CountryDTO dto = new(
                    country.Id,
                    country.Name,
                    country.ISO,
                    country.IsCountry
                );

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
