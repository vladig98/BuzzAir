namespace BuzzAir.Areas.Identity.Pages.Account.Models;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1515 // Consider making public types internal
public sealed class RegisterInputModel
#pragma warning restore CA1515 // Consider making public types internal
#pragma warning restore IDE0079 // Remove unnecessary suppression
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
    public string City { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Country")]
    public string Country { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Postal Code")]
    public string Postal { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Gender")]
    public Gender Gender { get; set; }
}
