namespace E_Commercial.ViewModel.Identity.Account;

public class RegisterViewModel
{
    [Required, EmailAddress(ErrorMessage = "this is not valid email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), MinLength(6, ErrorMessage = "Password must less than 6 character")]
    [Compare(nameof(Password), ErrorMessage = "the password and confirm password don not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Length(11, 11, ErrorMessage = "the Phone Number Must not be 11 number ")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;
    public string? Type { get; set; }
}