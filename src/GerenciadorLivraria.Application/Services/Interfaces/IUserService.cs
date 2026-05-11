using GerenciadorLivraria.Application.Dto;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Services.Interfaces
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