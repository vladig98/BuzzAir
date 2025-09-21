namespace BuzzAir.Services.Interfaces;

public interface ITravelDocumentService
{
    Task<TravelDocument?> CreateAsync(TravelDocumentDto? data, Gender gender, CancellationToken token);
}
