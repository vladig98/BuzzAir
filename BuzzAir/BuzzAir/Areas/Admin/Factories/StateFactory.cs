

namespace BuzzAir.Areas.Admin.Factories;

public sealed class StateFactory
{
    public static CreateStateVM BuildCreateStateVM(IList<CountryDTO> countries)
    {
        if (countries is null)
        {
            return new CreateStateVM();
        }

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        CreateStateVM model = new();

        foreach (CountryDTO country in countries)
        {
            model.Countries.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup
            });
        }

        return model;
    }

    public static EditStateVM BuildEditStateVM(StateDTO state, IList<CountryDTO> countries)
    {
        if (countries is null || state is null)
        {
            return new EditStateVM();
        }

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        EditStateVM model = new()
        {
            Id = state.Id,
            Name = state.Name
        };

        foreach (CountryDTO country in countries)
        {
            model.Countries.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup,
                Selected = state.Country == country.Name
            });

            if (state.Country == country.Name)
            {
                model.CountryId = country.Id;
            }
        }

        return model;
    }

    public static DeleteStateVM BuildDeleteStateVM(StateDTO state)
    {
        if (state is null)
        {
            return new DeleteStateVM();
        }

        DeleteStateVM model = new()
        {
            Id = state.Id,
            Name = state.Name,
            CountryName = state.Country
        };

        return model;
    }

    public static RestoreStateVM BuildRestoreStateVM(StateDTO state)
    {
        if (state is null)
        {
            return new RestoreStateVM();
        }

        RestoreStateVM model = new()
        {
            Id = state.Id,
            Name = state.Name,
            CountryName = state.Country
        };

        return model;
    }
}
