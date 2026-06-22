using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Application.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<BookEntity> Add(BookEntity bookEntity);
        BookEntity Update(BookEntity bookEntity);
        Task<bool> Delete(Guid id);
        Task<List<BookEntity>> GetAll();
        Task<List<BookEntity>> Get(int page, int size);
        Task<BookEntity?> GetById(Guid id);
        Task<BookEntity?> GetByName(string name);
        Task<BookEntity?> GetByTitleAndAuthor(string title, string author);
        Task<BookEntity?> GetByIsbn(string isbn);
        Task<bool> GetIsActiveByTitle(string title);
    }
}