using GerenciadorLivraria.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace GerenciadorLivraria.Infrastructure.Repository.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}