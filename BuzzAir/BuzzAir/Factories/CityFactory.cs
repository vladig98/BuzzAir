namespace BuzzAir.Factories
{
    public static class CityFactory
    {
        public static City Create(string name, Country country)
        {
            City city = new()
            {
                Country = country,
                Name = name
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
    }
}
