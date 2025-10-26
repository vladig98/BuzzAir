namespace BuzzAir.Areas.Admin.DTOs;

public sealed record class CountryDTO(string Id, string Name, string Iso2, string Iso3, bool IsOfficiallyRecognizedCountry);