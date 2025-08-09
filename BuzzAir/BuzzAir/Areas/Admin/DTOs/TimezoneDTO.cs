namespace BuzzAir.Areas.Admin.DTOs;

public sealed record class TimezoneDTO(string Id, string Name, TimeSpan Offset, string Identifier, string Abbreviation, bool UsesDST);
