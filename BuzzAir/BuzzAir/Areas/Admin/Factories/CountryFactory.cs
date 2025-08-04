namespace BuzzAir.Areas.Admin.Factories;

public static class CountryFactory
{
    public static EditCountryVM BuildEditCountryVM(CountryDTO country)
    {
        ArgumentNullException.ThrowIfNull(country);
        EditCountryVM model = new()
        {
            Id = country.Id,
            ISO = country.ISO,
            Name = country.Name,
            IsOfficiallyRecognizedCountry = country.IsOfficiallyRecognizedCountry
        };

        return model;
    }

    public static DeleteCountryVM BuildDeleteCountryVM(CountryDTO country)
    {
        ArgumentNullException.ThrowIfNull(country);
        DeleteCountryVM model = new()
        {
            Id = country.Id,
            ISO = country.ISO,
            Name = country.Name,
            IsOfficiallyRecognizedCountry = country.IsOfficiallyRecognizedCountry
        };

        return model;
    }

    public static RestoreCountryVM BuildRestoreCountryVM(CountryDTO country)
    {
        ArgumentNullException.ThrowIfNull(country);
        RestoreCountryVM model = new()
        {
            Id = country.Id,
            ISO = country.ISO,
            Name = country.Name,
            IsOfficiallyRecognizedCountry = country.IsOfficiallyRecognizedCountry
        };

        return model;
    }
}
