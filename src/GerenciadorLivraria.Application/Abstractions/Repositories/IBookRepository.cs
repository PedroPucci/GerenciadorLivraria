using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Application.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<BookEntity> Add(BookEntity bookEntity);
        BookEntity Update(BookEntity bookEntity);
        Task<bool> Delete(string id);
        Task<List<BookEntity>> Get();
        Task<BookEntity?> GetById(string id);
        Task<BookEntity?> GetByName(string name);
        Task<BookEntity?> GetByTitleAndAuthor(string title, string author);
        Task<BookEntity?> GetByIsbn(string isbn);
    }
}