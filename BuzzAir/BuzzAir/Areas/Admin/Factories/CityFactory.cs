namespace BuzzAir.Areas.Admin.Factories;

public static class CityFactory
{
    public static CreateCityVM BuildCreateCityVM(IList<CountryDTO> countries, IList<TimezoneDTO> timezones)
    {
        if (countries is null || timezones is null)
        {
            return new CreateCityVM();
        }

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        CreateCityVM model = new();

        foreach (CountryDTO country in countries)
        {
            model.Countries.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup
            });
        }

        foreach (TimezoneDTO timezone in timezones)
        {
            model.Timezones.Add(new SelectListItem()
            {
                Text = timezone.Name,
                Value = timezone.Id
            });
        }

        return model;
    }

    public static EditCityVM BuildEditCityVM(CityDTO city, IList<CountryDTO> countries, IList<StateDTO> states, IList<TimezoneDTO> timezones)
    {
        if (countries is null || timezones is null || city is null || states is null)
        {
            return new EditCityVM();
        }

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        EditCityVM model = new()
        {
            Id = city.Id,
            Name = city.Name
        };

        foreach (CountryDTO country in countries)
        {
            model.Countries.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup,
                Selected = country.Name == city.Country
            });

            if (country.Name == city.Country)
            {
                model.CountryId = country.Id;
            }
        }

        foreach (StateDTO state in states)
        {
            model.States.Add(new SelectListItem()
            {
                Text = state.Name,
                Value = state.Id,
                Selected = state.Name == city.State
            });

            if (state.Name == city.State)
            {
                model.StateId = state.Id;
            }
        }

        foreach (TimezoneDTO timezone in timezones)
        {
            model.Timezones.Add(new SelectListItem()
            {
                Text = timezone.Name,
                Value = timezone.Id,
                Selected = timezone.Name == city.Timezone
            });

            if (timezone.Name == city.Timezone)
            {
                model.TimezoneId = timezone.Id;
            }
        }

        return model;
    }

    public static DeleteCityVM BuildDeleteCityVM(CityDTO city)
    {
        if (city is null)
        {
            return new DeleteCityVM();
        }

        DeleteCityVM model = new()
        {
            Id = city.Id,
            Name = city.Name,
            CountryName = city.Country,
            StateName = city.State,
            TimezoneName = city.Timezone
        };

        return model;
    }

    public static RestoreCityVM BuildRestoreCityVM(CityDTO city)
    {
        if (city is null)
        {
            return new RestoreCityVM();
        }

        RestoreCityVM model = new()
        {
            Id = city.Id,
            Name = city.Name,
            CountryName = city.Country,
            StateName = city.State,
            TimezoneName = city.Timezone
        };

        return model;
    }
}
