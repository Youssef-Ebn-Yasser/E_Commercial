namespace E_Commercial.ViewModel.Identity.User;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "NationalId is required")]
    [Length(14, 14, ErrorMessage = "NationalId is must be 14 number")]

    public string NationalId { get; set; } = string.Empty;
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must less than 6 character")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Phone Number is required")]
    [Length(11, 11)]
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Type { get; set; }
}