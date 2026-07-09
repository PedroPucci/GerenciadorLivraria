using GerenciadorLivraria.Application.Abstractions.Cache;
using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Application.Abstractions.Services;
using GerenciadorLivraria.Application.Constants;
using GerenciadorLivraria.Application.Contracts.Dto.BookDto;
using GerenciadorLivraria.Application.Validators;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;
using GerenciadorLivraria.Shared.Logging;
using Serilog;

namespace GerenciadorLivraria.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private readonly ICacheService _cacheService;

        public BookService(
            IRepositoryUoW repositoryUoW,
            ICacheService cacheService)
        {
            _repositoryUoW = repositoryUoW;
            _cacheService = cacheService;
        }

        public async Task<Result<BookEntity>> Add(CreateBookRequestDto createBookRequestDto)
        {
            using var transaction = _repositoryUoW.BeginTransaction();

            try
            {
                var bookEntity = new BookEntity
                {
                    Title = createBookRequestDto.Title,
                    Author = createBookRequestDto.Author,
                    Genre = createBookRequestDto.Genre,
                    ISBN = createBookRequestDto.ISBN,
                    PublishedYear = createBookRequestDto.PublishedYear,
                    Publisher = createBookRequestDto.Publisher,
                    Description = createBookRequestDto.Description,
                    CreateDate = DateTime.UtcNow,
                    IsActive = true
                };

                var isValid = await IsValidBookRequest(createBookRequestDto);
                if (!isValid.Success)
                {
                    Log.Information(isValid.Message);
                    return Result<BookEntity>.Error(isValid.Message);
                }

                var existingBook = await _repositoryUoW.BookRepository
                    .GetByTitleAndAuthor(createBookRequestDto.Title, createBookRequestDto.Author);

                if (existingBook != null)
                {
                    Log.Information(LogMessages.BookAlreadyExistsError(
                        createBookRequestDto.Title,
                        createBookRequestDto.Author));

                    return Result<BookEntity>.Error("A book with the same title and author already exists.");
                }

                var existingISBN = await _repositoryUoW.BookRepository
                    .GetByIsbn(createBookRequestDto.ISBN);

                if (existingISBN != null)
                {
                    Log.Information(LogMessages.BookAlreadyExistsError(
                        createBookRequestDto.Title,
                        createBookRequestDto.Author));

                    return Result<BookEntity>.Error("A book with the same ISBN already exists.");
                }

                await _repositoryUoW.BookRepository.Add(bookEntity);
                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                await _cacheService.RemoveAsync(CacheKeys.BooksAll(1, 10));

                Log.Information(LogMessages.AddBookSuccess(bookEntity));

                return Result<BookEntity>.Ok(bookEntity);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                Log.Information(LogMessages.AddBookError(ex));

                throw;
            }
        }

        public Task<Result<bool>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookEntity>> Get(int page, int size)
        {
            var cacheKey = CacheKeys.BooksAll(page, size);

            var cachedBooks = await _cacheService
                .GetAsync<List<BookEntity>>(cacheKey);

            if (cachedBooks is not null)
            {
                Log.Information("Books loaded from Redis cache.");
                return cachedBooks;
            }

            try
            {
                var result = await _repositoryUoW
                    .BookRepository
                    .Get(page, size);

                if (!result.Success || result.Data == null)
                    throw new InvalidOperationException(result.Message);

                await _cacheService.SetAsync(
                    cacheKey,
                    result.Data,
                    TimeSpan.FromMinutes(5));

                Log.Information(LogMessages.GetAllBooksSuccess());

                return result.Data;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllBooksError(ex));

                throw new InvalidOperationException("Error to loading the list Book. See logs for details.", ex);
            }
        }

        public async Task<Result<BookEntity>> GetById(Guid id)
        {
            var cacheKey = CacheKeys.BookById(id);

            var cachedBook = await _cacheService.GetAsync<BookEntity>(cacheKey);

            if (cachedBook is not null)
                return Result<BookEntity>.Ok(cachedBook);

            try
            {
                var bookResult = await _repositoryUoW.BookRepository.GetById(id);

                if (!bookResult.Success || bookResult.Data == null)
                {
                    Log.Information("Book not found.");
                    return Result<BookEntity>.Error("Book not found.");
                }

                var book = bookResult.Data;

                if (!book.IsActive)
                {
                    Log.Information(LogMessages.BookAlreadyActiveError(book.Title));
                    return Result<BookEntity>.Error("Book is inactive.");
                }

                await _cacheService.SetAsync(
                    cacheKey,
                    book,
                    TimeSpan.FromMinutes(10));

                Log.Information(LogMessages.GetBookByIdSuccess(book));

                return Result<BookEntity>.Ok(book);
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetBookByIdError(ex));

                throw new InvalidOperationException(
                    "Error retrieving the book. See inner exception for details.",
                    ex);
            }
        }

        public async Task<Result<BookEntity>> GetByName(string name)
        {
            try
            {
                var bookResult = await _repositoryUoW.BookRepository.GetByName(name);

                if (!bookResult.Success || bookResult.Data == null)
                    return Result<BookEntity>.Error("Book not found.");

                var book = bookResult.Data;

                var bookEntity = new BookEntity
                {
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    Description = book.Description,
                    Stock = book.Stock,
                    IsActive = book.IsActive,
                };

                var isActiveBookResult = await _repositoryUoW.BookRepository
                    .GetIsActiveByTitle(bookEntity.Title);

                if (!isActiveBookResult.Success)
                    return Result<BookEntity>.Error(isActiveBookResult.Message);

                if (isActiveBookResult.Data)
                {
                    Log.Information(LogMessages.BookAlreadyActiveError(bookEntity.Title));
                    return Result<BookEntity>.Error("A book with the same title is already active.");
                }

                Log.Information(LogMessages.GetBookByIdSuccess(bookEntity));

                return Result<BookEntity>.Ok(bookEntity);
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetBookByIdError(ex));

                return Result<BookEntity>.Error(
                    "Error retrieving the book. See inner exception for details.");
            }
        }

        public Task<Result<bool>> Update(string id, UpdateBookRequestDto updateBookRequestDto)
        {
            throw new NotImplementedException();
        }

        private async Task<Result<CreateBookRequestDto>> IsValidBookRequest(CreateBookRequestDto createBookRequestDto)
        {
            var requestValidator = await new BookRequestValidator().ValidateAsync(createBookRequestDto);

            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<CreateBookRequestDto>.Error(errorMessage);
            }

            return Result<CreateBookRequestDto>.Ok();
        }
    }
}