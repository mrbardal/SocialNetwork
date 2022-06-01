namespace SocialNetwork.API.Models.FriendshipDtos
{
    public class UpdateFriendshipDto
    {
        public string Requester { get; set; }
        public string Addressee { get; set; }
        public int StatusId { get; set; }

    }
}
