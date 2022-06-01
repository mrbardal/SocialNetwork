using AutoMapper;
using SocialNetwork.Application.Featuers.UserFeature.Commands.Register;
using SocialNetwork.Infrastructure.Identity;

namespace SalesApp.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, AppUser>();
        CreateMap<AppUser, RegisterUserCommandResult>();
        //CreateMap<User, GetUserByIdQueryResult>();
        //CreateMap<User, GetUserByPriceQueryResult>();
    }
}
