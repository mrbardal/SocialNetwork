using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.Application.Featuers.UserFeature.Commands.Register;

public class RegisterUserCommandHandler :
    IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResult>>
{
    private UserManager<AppUser> _userManager;
    private IMapper _mapper;

    public RegisterUserCommandHandler(
        UserManager<AppUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<RegisterUserCommandResult>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user != null)
        {
            return await Task.FromResult(
                Result.Failure<RegisterUserCommandResult>("User Already registerd!"));
        }

        var newUser = _mapper.Map<AppUser>(request);

        var createResult = await _userManager.CreateAsync(newUser, request.Password);

        if (!createResult.Succeeded)
        {
            return await Task.FromResult(
                Result.Failure<RegisterUserCommandResult>(createResult.Errors.First().Description));
        }

        var result = _mapper.Map<RegisterUserCommandResult>(newUser);

        return await Task.FromResult(Result.Success(result));
    }
}
