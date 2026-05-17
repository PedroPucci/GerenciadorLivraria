using GerenciadorLivraria.Application.Contracts.Dto.UserDto;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Abstractions.Services
{
    public interface IAuthenticationUserService
    {
        Task<Result<string>> Login(UserForAuthenticationDTO userEntity);
    }
}