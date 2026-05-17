using FluentValidation;
using GerenciadorLivraria.Shared.DomainErrors;

namespace GerenciadorLivraria.Application.Validators
{
    public class UserRequestValidator : AbstractValidator<UserResponse>
    {
        public UserRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_EmailCanNotBeNullOrEmpty.Description())
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                    .WithMessage(UserErrors.User_Error_InvalidEmailFormat.Description());

            RuleFor(p => p.Password)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_PasswordCanNotBeNullOrEmpty.Description())
                .MinimumLength(8)
                    .WithMessage(UserErrors.User_Error_PasswordLengthLessEight.Description())
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$")
                    .WithMessage(UserErrors.User_Error_PasswordInvalid.Description());

            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_NameCanNotBeNullOrEmpty.Description())
                .MinimumLength(8)
                    .WithMessage(UserErrors.User_Error_NameLengthLessEight.Description());
        }
    }
}