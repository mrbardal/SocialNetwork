using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.FriendshipDtos;
using SocialNetwork.Domain.Aggregates.FriendshipAggregate;
using SocialNetwork.Infrastructure.Persistance.Core;

namespace SocialNetwork.Controllers;

[ApiController]
[Route("friendships")]
public class FriendshipController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFriendshipRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public FriendshipController(IUnitOfWork unitOfWork, IFriendshipRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> AddFriendship(AddFriendshipDto friendshipDto)
    {
        var friendship = _mapper.Map<Friendship>(friendshipDto);
        friendship.StatusId = FriendshipStatus.Requested.Id;

        await _unitOfWork.BeginAsync();
        await _repository.AddFriendshipAsync(friendship);
        await _unitOfWork.CommitAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFriendship(UpdateFriendshipDto friendshipDto)
    {
        var friendship = await _repository.GetFriendshipAsync(friendshipDto.Requester, friendshipDto.Addressee);

        if (friendship == null)
        {
            return BadRequest("Invalid Update");
        }

        friendship.StatusId = friendshipDto.StatusId;

        await _unitOfWork.BeginAsync();
        await _repository.UpdateFriendshipAsync(friendship);
        await _unitOfWork.CommitAsync();

        return Ok();
    }

    [HttpGet("requests")]
    public async Task<IActionResult> GetRequestedFriendshipsByUserName([FromQuery] string addressee)
    {
        var result = await _repository.GetFriendshipRequestsByUserNameAsync(addressee);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetFriendships([FromQuery] string requester, [FromQuery] string addressee)
    {
        var result = await _repository.GetFriendshipAsync(requester, addressee);

        //if (result == null)
        //{
        //    return NotFound();
        //}

        return Ok(result);
    }

    [HttpGet("followers")]
    public async Task<IActionResult> GetFollowersByUserName([FromQuery] string addressee)
    {
        var result = await _repository.GetFollowersByUserNameAsync(addressee);

        return Ok(result);
    }

    [HttpGet("followings")]
    public async Task<IActionResult> GetFollowingsByUserName([FromQuery] string requester)
    {
        var result = await _repository.GetFollowingsByUserNameAsync(requester);

        return Ok(result);
    }
}