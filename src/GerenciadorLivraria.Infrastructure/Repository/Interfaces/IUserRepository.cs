using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity userEntity);
        UserEntity Update(UserEntity userEntity);
        Task<bool> Delete(string id);
        Task<List<UserEntity>> Get();
        Task<UserEntity?> GetByIdCheck(string id);
        Task<bool> CheckPassword(UserEntity userEntity, string password);
        Task<UserEntity> GetByEmail(string email);
    }
}