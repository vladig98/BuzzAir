namespace BuzzAir.Areas.Admin.DTOs;

public sealed record class CountryDTO(string Id, string Name, bool IsOfficiallyRecognizedCountry);