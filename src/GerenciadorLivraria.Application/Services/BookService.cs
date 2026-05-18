using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Application.Abstractions.Services;
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

        public BookService(
            IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
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
                    Publisher  = createBookRequestDto.Publisher,
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

                var existingBook = await _repositoryUoW.BookRepository.GetByTitleAndAuthor(createBookRequestDto.Title, createBookRequestDto.Author);
                if (existingBook != null)
                {
                    Log.Information(LogMessages.BookAlreadyExistsError(createBookRequestDto.Title, createBookRequestDto.Author));
                    return Result<BookEntity>.Error("A book with the same title and author already exists.");
                }

                var existingISBN = await _repositoryUoW.BookRepository.GetByIsbn(createBookRequestDto.ISBN);
                if (existingISBN != null)
                {
                    Log.Information(LogMessages.BookAlreadyExistsError(createBookRequestDto.Title, createBookRequestDto.Author));
                    return Result<BookEntity>.Error("A book with the same ISBN already exists.");
                }

                await _repositoryUoW.BookRepository.Add(bookEntity);
                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
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

        public async Task<List<BookEntity>> Get()
        {
            using var transaction = _repositoryUoW.BeginTransaction();

            try
            {
                List<BookEntity> bookEntities = await _repositoryUoW.BookRepository.Get();
                _repositoryUoW.Commit();

                Log.Information(LogMessages.GetAllBooksSuccess());
                return bookEntities;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(LogMessages.GetAllBooksError(ex));
                throw new InvalidOperationException("Error to loading the list Book. See logs for details.", ex);
            }
        }

        public Task<Result<BookResponseDto>> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<BookResponseDto>> GetByName(string name)
        {
            throw new NotImplementedException();
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