using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.UserDtos;
using SocialNetwork.Application.Featuers.UserFeature.Commands.Register;
using SocialNetwork.Application.Featuers.UserFeature.Queries.GetToken;
using SocialNetwork.Application.Featuers.UserFeature.Queries.Search;

namespace SocialNetwork.Controllers;

//[AllowAnonymous]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto user)
    {
        var command = _mapper.Map<RegisterUserCommand>(user);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto user)
    {
        var query = _mapper.Map<GetTokenQuery>(user);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        var query = new SearchQuery(name);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }
}