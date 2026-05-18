using FluentValidation;
using GerenciadorLivraria.Application.Contracts.DomainErrors;
using GerenciadorLivraria.Application.Contracts.Dto.UserDto;
using GerenciadorLivraria.Shared.Helpers;

namespace GerenciadorLivraria.Application.Validators
{
    public class UserRequestValidator : AbstractValidator<CreateUserRequestDto>
    {
        public UserRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage(UserErrors.User_Error_NameCanNotBeNullOrEmpty.Description())
                .MinimumLength(8)
                    .WithMessage(UserErrors.User_Error_NameLengthLessEight.Description());
        }
    }
}