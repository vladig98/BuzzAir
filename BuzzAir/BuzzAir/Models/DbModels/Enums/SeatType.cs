namespace BuzzAir.Models.DbModels.Enums
{
    public enum SeatType
    {
        Normal = 1,
        [CustomDisplay(Name = "Extra Leg Room")]
        Extra_Leg_Room = 2
    }
}