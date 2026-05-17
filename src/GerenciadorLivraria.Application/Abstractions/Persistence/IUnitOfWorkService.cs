using GerenciadorLivraria.Application.Services;


namespace GerenciadorLivraria.Application.Abstractions.Persistence
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        AuthenticationService AuthenticationService { get; }
    }
}