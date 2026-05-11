using GerenciadorLivraria.Application.Services;


namespace GerenciadorLivraria.Application.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        AuthenticationService AuthenticationService { get; }
    }
}