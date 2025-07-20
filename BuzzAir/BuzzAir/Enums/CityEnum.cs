namespace BuzzAir.Enums
{
    [Flags]
    public enum CityEnum
    {
        None = 0,
        Country = 1 << 0,
        State = 1 << 1,
        All = 3
    }
}
