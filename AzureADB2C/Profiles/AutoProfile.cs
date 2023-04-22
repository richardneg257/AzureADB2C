using AutoMapper;
using AzureADB2C.Models;
using Microsoft.Graph.Models;

namespace AzureADB2C.Profiles;

public class AutoProfile : Profile
{
    public AutoProfile()
    {
        CreateMap<User, GetUserModel>();
    }
}
