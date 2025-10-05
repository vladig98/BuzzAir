namespace BuzzAir.JsonDataDTOs;

public class CountryJsonDto
{
    public string Name { get; set; } = string.Empty;
    public string ISOA2 { get; set; } = string.Empty;
    public string ISOA3 { get; set; } = string.Empty;
    public bool IsOfficiallyRecognizedCountry { get; set; }
}
