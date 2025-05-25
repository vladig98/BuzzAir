namespace BuzzAir.Factories
{
    public static class CountryFactory
    {
        public static List<SelectListItem> GetCountriesForSelect(Country[] countryModels)
        {
            SelectListGroup countryGroup = new()
            {
                Name = "Countries"
            };
            SelectListGroup dependenciesGroup = new()
            {
                Name = "Dependencies and territories not offically recognized as countries"
            };

            int count = countryModels.Length;
            List<SelectListItem> countries = new(count);

            for (int i = 0; i < count; i++)
            {
                Country country = countryModels[i];

                SelectListItem countryListItem = new()
                {
                    Text = country.Name,
                    Value = country.Id,
                    Group = country.IsCountry ? countryGroup : dependenciesGroup
                };

                countries.Add(countryListItem);
            }

            countries = [.. countries.OrderBy(x => x.Group.Name).ThenBy(x => x.Text)];

            return countries;
        }
    }
}
