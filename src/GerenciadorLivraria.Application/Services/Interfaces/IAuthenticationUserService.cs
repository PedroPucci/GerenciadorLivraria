using GerenciadorLivraria.Application.Dto;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Services.Interfaces
{
    public interface IAuthenticationUserService
    {
        Task<Result<string>> Login(UserForAuthenticationDTO userEntity);
    }
}