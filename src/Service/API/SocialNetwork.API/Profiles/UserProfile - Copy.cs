using AutoMapper;
using SocialNetwork.API.Models.FriendshipDtos;
using SocialNetwork.Domain.Aggregates.FriendshipAggregate;

namespace SocialNetwork.API.Profiles
{
    public class FriendshipProfile : Profile
    {
        public FriendshipProfile()
        {
            CreateMap<AddFriendshipDto, Friendship>();
        }
    }
}
