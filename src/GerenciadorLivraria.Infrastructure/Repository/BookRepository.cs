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

        public async Task<bool> Delete(Guid id)
        {
            var book = await GetById(id);

            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BookEntity>> GetAll()
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

        public async Task<List<BookEntity>> Get(int page, int size)
        {
            return await _context.Books
                .AsNoTracking()
                .OrderBy(book => book.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(book => new BookEntity
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Genre = book.Genre,
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                    Publisher = book.Publisher,
                    Stock = book.Stock,
                    IsActive = book.IsActive,
                    CreateDate = book.CreateDate,
                    ModificationDate = book.ModificationDate
                })
                .ToListAsync();
        }

        public async Task<BookEntity?> GetById(Guid id)
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

        public async Task<BookEntity?> GetByTitleAndAuthor(string title, string author)
        {
            return await _context.Books
                .FirstOrDefaultAsync(book =>
                    book.Title == title &&
                    book.Author == author);
        }

        public async Task<BookEntity?> GetByIsbn(string isbn)
        {
            return await _context.Books
                .FirstOrDefaultAsync(book => book.ISBN == isbn);
        }

        public async Task<bool> GetIsActiveByTitle(string title)
        {
            return await _context.Books
                .Where(book => book.Title == title)
                .Select(book => book.IsActive)
                .FirstOrDefaultAsync();
        }
    }
}