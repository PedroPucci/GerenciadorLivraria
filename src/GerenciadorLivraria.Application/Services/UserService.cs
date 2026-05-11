using GerenciadorLivraria.Application.Dto;
using GerenciadorLivraria.Application.Services.Interfaces;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;
using GerenciadorLivraria.Infrastructure.Repository.RepositoryUoW;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorLivraria.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<ProfileEntity> _roleManager;

        public UserService(
            IRepositoryUoW repositoryUoW,
            UserManager<UserEntity> userManager,
            RoleManager<ProfileEntity> roleManager)
        {
            _repositoryUoW = repositoryUoW;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<Result<UserEntity>> Add(CreateUserRequestDto createUserRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserResponseDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserResponseDto>> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(string id, UpdateUserRequestDto updateUserRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}