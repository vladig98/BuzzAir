namespace BuzzAir.Areas.Identity.Pages.Account.Models;

public sealed class RegisterInputModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Display(Name = "FullName")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Street")]
    public string Street { get; set; } = string.Empty;

    [Required]
    [Display(Name = "City")]
    public string CityId { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Country")]
    public string CountryId { get; set; } = string.Empty;

    [Required]
    [Display(Name = "State")]
    public string? StateId { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Postal Code")]
    public string Postal { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Gender")]
    public Gender Gender { get; set; }
}
