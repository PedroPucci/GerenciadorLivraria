using GerenciadorLivraria.Application.Abstractions.Repositories;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.Connections;

namespace GerenciadorLivraria.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public Task<BookEntity> Add(BookEntity bookEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<BookEntity?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BookEntity?> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public BookEntity Update(BookEntity bookEntity)
        {
            throw new NotImplementedException();
        }
    }
}