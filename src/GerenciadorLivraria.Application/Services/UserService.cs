using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Application.Abstractions.Services;
using GerenciadorLivraria.Application.Contracts.Dto.UserDto;
using GerenciadorLivraria.Application.Validators;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;
using GerenciadorLivraria.Shared.Logging;
using Microsoft.AspNetCore.Identity;
using Serilog;

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

        public async Task<Result<UserEntity>> Add(CreateUserRequestDto createUserRequestDto)
        {
            using var transaction = _repositoryUoW.BeginTransaction();

            try
            {
                var userEntity = new UserEntity
                {
                    Email = createUserRequestDto.Email,
                    Name = createUserRequestDto.Name,
                    UserName = createUserRequestDto.Email,
                    CreateDate = DateTime.UtcNow,
                    IsActive = true
                };

                var isValid = await IsValidUserRequest(createUserRequestDto);
                if (!isValid.Success)
                {
                    Log.Information(isValid.Message);
                    return Result<UserEntity>.Error(isValid.Message);
                }

                if (string.IsNullOrWhiteSpace(createUserRequestDto.Role))
                {
                    Log.Information("'Role' can not be null or empty!");
                    return Result<UserEntity>.Error("'Role' can not be null or empty!");
                }

                var role = createUserRequestDto.Role.Trim();

                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    Log.Information("Invalid role.");
                    return Result<UserEntity>.Error("Invalid role. Use only: Administrator or Usuario.");
                }

                var createResult = await _userManager.CreateAsync(userEntity, createUserRequestDto.Password!);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(" ", createResult.Errors.Select(e => e.Description));
                    return Result<UserEntity>.Error(errors);
                }

                var roleResult = await _userManager.AddToRoleAsync(userEntity, role);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(" ", roleResult.Errors.Select(e => e.Description));
                    return Result<UserEntity>.Error(errors);
                }

                await transaction.CommitAsync();
                Log.Information(LogMessages.AddUserSuccess(userEntity));
                return Result<UserEntity>.Ok(userEntity);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Log.Information(LogMessages.AddUserError(ex));
                throw;
            }
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

        private async Task<Result<CreateUserRequestDto>> IsValidUserRequest(CreateUserRequestDto createUserRequestDto)
        {
            var requestValidator = await new UserRequestValidator().ValidateAsync(createUserRequestDto);

            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<CreateUserRequestDto>.Error(errorMessage);
            }

            return Result<CreateUserRequestDto>.Ok();
        }
    }
}