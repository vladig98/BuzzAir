namespace BuzzAir.Areas.Admin.ViewModels.StateDTOs
{
    public class DeleteStateVM
    {
        [Required]
        [HiddenInput]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }
}
