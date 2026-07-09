using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;
using GerenciadorLivraria.Infrastructure.Connections;
using GerenciadorLivraria.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<UserEntity>> Add(UserEntity userEntity)
        {
            try
            {
                var result = await _userManager.CreateAsync(userEntity, userEntity.PasswordHash);

                if (!result.Succeeded)
                {
                    return Result<UserEntity>.Error(
                        string.Join(" | ", result.Errors.Select(e => e.Description)));
                }

                return Result<UserEntity>.Ok(
                    "User created successfully.",
                    userEntity);
            }
            catch (Exception ex)
            {
                return Result<UserEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<bool>> CheckPassword(UserEntity userEntity, string password)
        {
            try
            {
                var result = await _userManager.CheckPasswordAsync(userEntity, password);

                if (!result)
                    return Result<bool>.Error("Invalid password.");

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(string id)
        {
            try
            {
                var result = await GetByIdCheck(id);

                if (!result.Success || result.Data == null)
                    return Result<bool>.Error("User not found.");

                _context.Users.Remove(result.Data);
                await _context.SaveChangesAsync();

                return Result<bool>.Ok(
                    responseMessage: "User deleted successfully.",
                    responseData: true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(ex.Message);
            }
        }

        public async Task<Result<List<UserEntity>>> Get()
        {
            try
            {
                var users = await (
                    from user in _context.Users.AsNoTracking()
                    join userRole in _context.UserRoles.AsNoTracking()
                        on user.Id equals userRole.UserId
                    join role in _context.Roles.AsNoTracking()
                        on userRole.RoleId equals role.Id
                    orderby user.Id
                    select new UserEntity
                    {
                        Email = user.Email,
                        Name = user.Name,
                        IsActive = user.IsActive
                    }
                ).ToListAsync();

                return Result<List<UserEntity>>.Ok(users);
            }
            catch (Exception ex)
            {
                return Result<List<UserEntity>>.Error(ex.Message);
            }
        }

        public async Task<Result<UserEntity>> GetByEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return Result<UserEntity>.Error("Usuário não encontrado.");

                return Result<UserEntity>.Ok(user);
            }
            catch (Exception ex)
            {
                return Result<UserEntity>.Error($"Erro ao buscar usuário: {ex.Message}");
            }
        }

        public async Task<Result<UserEntity>> GetByIdCheck(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    return Result<UserEntity>.Error("User not found.");

                return Result<UserEntity>.Ok(user);
            }
            catch (Exception ex)
            {
                return Result<UserEntity>.Error(ex.Message);
            }
        }

        public Result<UserEntity> Update(UserEntity userEntity)
        {
            try
            {
                var user = _context.Users.Update(userEntity).Entity;

                return Result<UserEntity>.Ok(
                    "User updated successfully.",
                    user);
            }
            catch (Exception ex)
            {
                return Result<UserEntity>.Error(ex.Message);
            }
        }
    }
}