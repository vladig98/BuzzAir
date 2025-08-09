namespace BuzzAir.Areas.Admin.Factories;

public static class AircraftFactory
{
    public static DeleteAircraftVM BuildDeleteAircraftVM(AircraftDTO aircraft)
    {
        ArgumentNullException.ThrowIfNull(aircraft);

        DeleteAircraftVM model = new()
        {
            Id = aircraft.Id,
            Name = aircraft.Name,
            NumberOfSeats = aircraft.NumberOfSeats
        };

        return model;
    }

    public static EditAircraftVM BuildEditAircraftVM(AircraftDTO aircraft)
    {
        ArgumentNullException.ThrowIfNull(aircraft);

        EditAircraftVM model = new()
        {
            Id = aircraft.Id,
            Name = aircraft.Name,
            NumberOfSeats = aircraft.NumberOfSeats
        };

        return model;
    }

    public static RestoreAircraftVM BuildRestoreAircraftVM(AircraftDTO aircraft)
    {
        ArgumentNullException.ThrowIfNull(aircraft);

        RestoreAircraftVM model = new()
        {
            Id = aircraft.Id,
            Name = aircraft.Name,
            NumberOfSeats = aircraft.NumberOfSeats
        };

        return model;
    }
}
