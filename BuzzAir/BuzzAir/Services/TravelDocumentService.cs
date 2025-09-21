namespace BuzzAir.Services;

public class TravelDocumentService(
    BuzzAirDbContext dbContext,
    ICountryService countryService) : ITravelDocumentService
{
    public async Task<TravelDocument?> CreateAsync(TravelDocumentDto? data, Gender gender, CancellationToken token)
    {
        if (data is null)
        {
            return null!;
        }

        bool isValidDocumentType = Enum.TryParse(data.DocumentType, ignoreCase: true, out DocumentType document);

        if (!isValidDocumentType)
        {
            throw new InvalidOperationException("Invalid travel document");
        }

        Country nationalCountry = await countryService.GetCountryModelByIdAsync(data.NationalityId, token);
        Country birthCountry = await countryService.GetCountryModelByIdAsync(data.BirthCountryId, token);

        TravelDocument travelDocument = new()
        {
            ExpiryDate = data.ExpiryDate,
            Gender = gender,
            IssueDate = data.IssueDate,
            Number = data.Number,
            Type = document,
            BirthCountry = birthCountry,
            Nationality = nationalCountry
        };

        _ = await dbContext.TravelDocuments.AddAsync(travelDocument, token);

        return travelDocument;
    }
}
