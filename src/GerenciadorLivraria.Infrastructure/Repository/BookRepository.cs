using GerenciadorLivraria.Application.Abstractions.Repositories;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;
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

        public async Task<Result<BookEntity>> Add(BookEntity bookEntity)
        {
            try
            {
                await _context.Books.AddAsync(bookEntity);
                await _context.SaveChangesAsync();

                return Result<BookEntity>.Ok(
                    "Book created successfully.",
                    bookEntity);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                var book = await GetById(id);

                if (!book.Success || book.Data == null)
                    return Result<bool>.Error("Book not found.");

                _context.Books.Remove(book.Data);
                await _context.SaveChangesAsync();

                return Result<bool>.Ok(
                    "Book deleted successfully.",
                    true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(ex.Message);
            }
        }

        public async Task<Result<List<BookEntity>>> GetAll()
        {
            try
            {
                var books = await _context.Books
                    .AsNoTracking()
                    .OrderBy(book => book.Id)
                    .ToListAsync();

                return Result<List<BookEntity>>.Ok(books);
            }
            catch (Exception ex)
            {
                return Result<List<BookEntity>>.Error(ex.Message);
            }
        }

        public async Task<Result<List<BookEntity>>> Get(int page, int size)
        {
            try
            {
                var books = await _context.Books
                    .AsNoTracking()
                    .OrderBy(book => book.Id)
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync();

                return Result<List<BookEntity>>.Ok(books);
            }
            catch (Exception ex)
            {
                return Result<List<BookEntity>>.Error(ex.Message);
            }
        }

        public async Task<Result<BookEntity>> GetById(Guid id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                    return Result<BookEntity>.Error("Book not found.");

                return Result<BookEntity>.Ok(book);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<BookEntity>> GetByName(string name)
        {
            try
            {
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(book => book.Title == name);

                if (book == null)
                    return Result<BookEntity>.Error("Book not found.");

                return Result<BookEntity>.Ok(book);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public Result<BookEntity> Update(BookEntity bookEntity)
        {
            try
            {
                var book = _context.Books.Update(bookEntity).Entity;

                return Result<BookEntity>.Ok(
                    "Book updated successfully.",
                    book);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<BookEntity>> GetByTitleAndAuthor(string title, string author)
        {
            try
            {
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(book =>
                        book.Title == title &&
                        book.Author == author);

                if (book == null)
                    return Result<BookEntity>.Error("Book not found.");

                return Result<BookEntity>.Ok(book);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<BookEntity>> GetByIsbn(string isbn)
        {
            try
            {
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(book => book.ISBN == isbn);

                if (book == null)
                    return Result<BookEntity>.Error("Book not found.");

                return Result<BookEntity>.Ok(book);
            }
            catch (Exception ex)
            {
                return Result<BookEntity>.Error(ex.Message);
            }
        }

        public async Task<Result<bool>> GetIsActiveByTitle(string title)
        {
            try
            {
                var result = await _context.Books
                    .Where(book => book.Title == title)
                    .Select(book => book.IsActive)
                    .FirstOrDefaultAsync();

                return Result<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(ex.Message);
            }
        }
    }
}