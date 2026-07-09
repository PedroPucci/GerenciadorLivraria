using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<UserEntity>> Add(UserEntity userEntity);
        Result<UserEntity> Update(UserEntity userEntity);
        Task<Result<bool>> Delete(string id);
        Task<Result<List<UserEntity>>> Get();
        Task<Result<UserEntity>> GetByIdCheck(string id);
        Task<Result<bool>> CheckPassword(UserEntity userEntity, string password);
        Task<Result<UserEntity>> GetByEmail(string email);
    }
}