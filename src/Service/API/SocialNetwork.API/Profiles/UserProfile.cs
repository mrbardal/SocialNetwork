using AutoMapper;
using SocialNetwork.API.Models.UserDtos;
using SocialNetwork.Application.Featuers.UserFeature.Commands.Register;
using SocialNetwork.Application.Featuers.UserFeature.Queries.GetToken;
using SocialNetwork.Application.Featuers.UserFeature.Queries.Search;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, AppUser>();
            CreateMap<LoginUserDto, GetTokenQuery>();
            CreateMap<SearchQueryResult, SearchUserDto>();
        }
    }
}
