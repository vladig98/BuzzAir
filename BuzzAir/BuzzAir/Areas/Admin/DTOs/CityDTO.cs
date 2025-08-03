namespace BuzzAir.Areas.Admin.DTOs;

public record class CityDTO(string Id, string Name, string Country, string? State, string Timezone);
