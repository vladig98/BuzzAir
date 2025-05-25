namespace BuzzAir.Models.ViewModels
{
    public record PassengerViewModel
    {
        [Required]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The first name must contain only latin letters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The last name must contain only latin letters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public BaggageType BaggageType { get; set; }
        public List<ServiceViewModel> Services { get; set; } = [];
    }
}
