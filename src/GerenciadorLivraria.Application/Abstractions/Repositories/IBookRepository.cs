using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<Result<BookEntity>> Add(BookEntity bookEntity);
        Result<BookEntity> Update(BookEntity bookEntity);
        Task<Result<bool>> Delete(Guid id);
        Task<Result<List<BookEntity>>> GetAll();
        Task<Result<List<BookEntity>>> Get(int page, int size);
        Task<Result<BookEntity>> GetById(Guid id);
        Task<Result<BookEntity>> GetByName(string name);
        Task<Result<BookEntity>> GetByTitleAndAuthor(string title, string author);
        Task<Result<BookEntity>> GetByIsbn(string isbn);
        Task<Result<bool>> GetIsActiveByTitle(string title);
    }
}