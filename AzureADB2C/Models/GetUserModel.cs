using Microsoft.Graph.Models;

namespace AzureADB2C.Models;

public class GetUserModel
{
    public GetUserModel()
    {
        Identities = new List<ObjectIdentity>();
        AdditionalData = new List<KeyValuePair<string, string>>();
    }

    public Guid Id { get; set; }
    public string GivenName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<ObjectIdentity> Identities { get; set; }
    public List<KeyValuePair<string, string>> AdditionalData { get; set; }
}
