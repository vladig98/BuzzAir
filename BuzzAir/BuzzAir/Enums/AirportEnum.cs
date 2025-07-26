namespace BuzzAir.Enums
{
    [Flags]
    public enum AirportEnum : uint
    {
        None = 0,
        City = 1 << 0,
        Country = 1 << 1,
        All = 3,
        State = 1 << 2
    }
}
