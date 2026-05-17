using GerenciadorLivraria.Application.Services;


namespace GerenciadorLivraria.Application.Abstractions.Persistence
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        BookService BookService { get; }
        AuthenticationService AuthenticationService { get; }
    }
}