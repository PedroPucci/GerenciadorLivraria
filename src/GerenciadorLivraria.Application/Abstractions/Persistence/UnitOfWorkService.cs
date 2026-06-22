using GerenciadorLivraria.Application.Abstractions.Cache;
using GerenciadorLivraria.Application.Services;
using GerenciadorLivraria.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorLivraria.Application.Abstractions.Persistence
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<ProfileEntity> _roleManager;
        private UserService userService;
        private BookService bookService;
        private AuthenticationService authenticationService;
        private readonly ICacheService _cacheService;

        public UnitOfWorkService(
            IRepositoryUoW repositoryUoW,
            UserManager<UserEntity> userManager,
            RoleManager<ProfileEntity> roleManager,
            ICacheService cacheService)
        {
            _repositoryUoW = repositoryUoW;
            _userManager = userManager; 
            _roleManager = roleManager;
            _cacheService = cacheService;
        }

        public UserService UserService
        {
            get
            {
                if (userService is null)
                    userService = new UserService(
                        _repositoryUoW,
                        _userManager,
                        _roleManager);
                return userService;
            }
        }

        public BookService BookService
        {
            get
            {
                if (bookService is null)
                    bookService = new BookService(
                        _repositoryUoW, _cacheService);
                return bookService;
            }
        }

        public AuthenticationService AuthenticationService
        {
            get
            {
                if (authenticationService is null)
                    authenticationService = new AuthenticationService(
                        _repositoryUoW,
                        _userManager);
                return authenticationService;
            }
        }
    }
}