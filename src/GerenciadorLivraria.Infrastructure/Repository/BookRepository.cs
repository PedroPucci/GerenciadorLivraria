using GerenciadorLivraria.Application.Abstractions.Repositories;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<BookEntity> Add(BookEntity bookEntity)
        {
            var result = await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();
            return bookEntity;
        }

        public async Task<bool> Delete(string id)
        {
            var book = await GetById(id);

            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BookEntity>> Get()
        {
            return await _context.Books
            .AsNoTracking()
            .OrderBy(game => game.Id)
            .Select(game => new BookEntity
            {
                Title = game.Title,
                Description = game.Description
            })
            .ToListAsync();
        }

        public async Task<BookEntity?> GetById(string id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<BookEntity?> GetByName(string name)
        {
            return await _context.Books.FindAsync(name);
        }

        public BookEntity Update(BookEntity bookEntity)
        {
            return _context.Books.Update(bookEntity).Entity;
        }
    }
}