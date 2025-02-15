namespace E_Commercial.ViewModel.Identity.User;

public class ShowAllUsersViewModel
{
    public string Id { get; set; } 
    public string NationalId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
}