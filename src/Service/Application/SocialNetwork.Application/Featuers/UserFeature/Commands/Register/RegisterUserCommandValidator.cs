using FluentValidation;

namespace SocialNetwork.Application.Featuers.UserFeature.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.UserName)
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Invalid UserName!");

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Invalid Password");
    }
}
