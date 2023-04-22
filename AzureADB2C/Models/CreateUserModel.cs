namespace AzureADB2C.Models;

public class CreateUserModel
{
    public string GivenName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public string Roles { get; set; } = string.Empty;
}
