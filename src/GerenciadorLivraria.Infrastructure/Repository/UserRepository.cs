using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.Connections;
using GerenciadorLivraria.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorLivraria.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public UserRepository(
            DataContext context, 
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<UserEntity> Add(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPassword(UserEntity userEntity, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity?> GetByIdCheck(string id)
        {
            throw new NotImplementedException();
        }

        public UserEntity Update(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}