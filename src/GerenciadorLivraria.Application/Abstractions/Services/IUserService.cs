using GerenciadorLivraria.Application.Contracts.Dto.UserDto;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<Result<UserEntity>> Add(CreateUserRequestDto createUserRequestDto);
        Task<Result<bool>> Update(string id, UpdateUserRequestDto updateUserRequestDto);
        Task<Result<bool>> Delete(string id);
        Task<List<UserResponseDto>> Get();
        Task<Result<UserResponseDto>> GetById(string id);
    }
}