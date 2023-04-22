using AutoMapper;
using Azure.Identity;
using AzureADB2C.Models;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AzureADB2C.Services;

public class UserService : IUserService
{
    const string customAttributeName1 = "OrganizationId";
    const string customAttributeName2 = "Roles";

    private readonly GraphServiceClient _graphClient;
    private readonly string? _tenantId;
    private readonly string? _b2cExtensionAppClientId;
    private readonly string _organizationIdAttributeName;
    private readonly string _rolesAttributeName;
    private readonly IMapper _mapper;

    public UserService(IConfiguration config, IMapper mapper)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };
        var clientSecretCredential = new ClientSecretCredential(config.GetValue<string>("B2C:TenantId"), config.GetValue<string>("B2C:AppId"), config.GetValue<string>("B2C:ClientSecret"));
        _graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        _tenantId = config.GetValue<string>("B2C:TenantId");
        _b2cExtensionAppClientId = config.GetValue<string>("B2C:B2cExtensionAppClientId") ?? throw new ArgumentException("B2C Extension App ClientId (ApplicationId) is missing in the appsettings.json");

        var helper = new Helpers.B2cCustomAttributeHelper(_b2cExtensionAppClientId);
        _organizationIdAttributeName = helper.GetCompleteAttributeName(customAttributeName1);
        _rolesAttributeName = helper.GetCompleteAttributeName(customAttributeName2);
        _mapper = mapper;
    }

    public async Task<List<GetUserModel>> GetUsers()
    {
        var users = await _graphClient.Users
            .GetAsync(x => x.QueryParameters.Select = new string[] {
                    "id",
                    "givenName",
                    "surName",
                    "displayName",
                    "identities",
                    $"{_organizationIdAttributeName}",
                    $"{_rolesAttributeName}"
            });

        return _mapper.Map<List<GetUserModel>>(users?.Value);
    }

    public async Task<GetUserModel> GetUserById(Guid id)
    {
        var user = await _graphClient.Users[id.ToString()]
            .GetAsync(x => x.QueryParameters.Select = new string[] {
                    "id",
                    "givenName",
                    "surName",
                    "displayName",
                    "identities",
                    $"{_organizationIdAttributeName}",
                    $"{_rolesAttributeName}"
            });

        return _mapper.Map<GetUserModel>(user);
    }

    public async Task<List<GetUserModel>> GetUserBySignInName(string signInName)
    {
        var users = await _graphClient.Users
            .GetAsync(x =>
            {
                x.QueryParameters.Select = new string[] {
                            "id",
                            "givenName",
                            "surName",
                            "displayName",
                            "identities",
                            $"{_organizationIdAttributeName}",
                            $"{_rolesAttributeName}"
                };
                x.QueryParameters.Filter = $"identities/any(c:c/issuerAssignedId eq '{signInName}' and c/issuer eq '{_tenantId}')";
            });

        return _mapper.Map<List<GetUserModel>>(users?.Value);
    }

    public async Task CreateUser(List<CreateUserModel> userList)
    {
        foreach (var user in userList)
        {
            var extensionInstance = new Dictionary<string, object>();
            extensionInstance.Add(_organizationIdAttributeName, user.OrganizationId);
            extensionInstance.Add(_rolesAttributeName, user.Roles);

            await _graphClient.Users
                .PostAsync(new User
                {
                    GivenName = user.GivenName,
                    Surname = user.Surname,
                    DisplayName = user.DisplayName,
                    Identities = new List<ObjectIdentity>
                    {
                    new ObjectIdentity()
                    {
                        SignInType = "emailAddress",
                        Issuer = _tenantId,
                        IssuerAssignedId = user.Email
                    }
                    },
                    PasswordProfile = new PasswordProfile()
                    {
                        ForceChangePasswordNextSignIn = false,
                        Password = user.Password
                    },
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = extensionInstance
                });
        }
    }

    public async Task DeleteUserById(Guid id)
    {
        await _graphClient.Users[id.ToString()].DeleteAsync();
    }
}
