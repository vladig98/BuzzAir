namespace BuzzAir.Areas.Admin.Factories;

public static class TimezoneFactory
{
    public static DeleteTimezoneVM BuildDeleteTimezoneVM(TimezoneDTO timezone)
    {
        ArgumentNullException.ThrowIfNull(timezone);

        DeleteTimezoneVM model = new()
        {
            Abbreviation = timezone.Abbreviation,
            Identifier = timezone.Identifier,
            Name = timezone.Name,
            Offset = timezone.Offset,
            UsesDST = timezone.UsesDST,
            Id = timezone.Id
        };

        return model;
    }

    public static EditTimezoneVM BuildEditTimezoneVM(TimezoneDTO timezone)
    {
        ArgumentNullException.ThrowIfNull(timezone);

        EditTimezoneVM model = new()
        {
            Abbreviation = timezone.Abbreviation,
            Identifier = timezone.Identifier,
            Name = timezone.Name,
            Offset = timezone.Offset,
            UsesDST = timezone.UsesDST,
            Id = timezone.Id
        };

        return model;
    }

    public static RestoreTimezoneVM BuildRestoreTimezoneVM(TimezoneDTO timezone)
    {
        ArgumentNullException.ThrowIfNull(timezone);

        RestoreTimezoneVM model = new()
        {
            Abbreviation = timezone.Abbreviation,
            Identifier = timezone.Identifier,
            Name = timezone.Name,
            Offset = timezone.Offset,
            UsesDST = timezone.UsesDST,
            Id = timezone.Id
        };

        return model;
    }
}
