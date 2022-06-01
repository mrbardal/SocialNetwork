using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.Application.Featuers.UserFeature.Queries.GetToken;

public class GetTokenQueryHandler :
    IRequestHandler<GetTokenQuery, Result<GetTokenQueryResult>>
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private JwtService _jwtService;
    private IMapper _mapper;

    public GetTokenQueryHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        JwtService jwtService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<Result<GetTokenQueryResult>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return await Task.FromResult(
                Result.Failure<GetTokenQueryResult>("User not found"));
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return await Task.FromResult(
                Result.Failure<GetTokenQueryResult>("Password is incorrent"));
        }

        var token = await _jwtService.GenerateAsync(user);

        var result = new GetTokenQueryResult(request.UserName, token);

        return await Task.FromResult(Result.Success(result));
    }
}
