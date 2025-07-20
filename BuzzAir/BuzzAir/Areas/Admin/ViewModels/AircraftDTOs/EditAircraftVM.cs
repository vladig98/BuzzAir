namespace BuzzAir.Areas.Admin.ViewModels.AircraftDTOs
{
    public class EditAircraftVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
    }
}
