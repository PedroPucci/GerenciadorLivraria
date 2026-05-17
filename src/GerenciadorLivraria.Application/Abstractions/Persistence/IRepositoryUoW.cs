using GerenciadorLivraria.Application.Abstractions.Repositories;
using GerenciadorLivraria.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorLivraria.Application.Abstractions.Persistence
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }
        IBookRepository BookRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}