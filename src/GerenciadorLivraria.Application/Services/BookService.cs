using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Application.Abstractions.Services;
using GerenciadorLivraria.Application.Contracts.Dto.BookDto;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

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
        public Task<Result<BookEntity>> Add(CreateBookRequestDto createBookRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookResponseDto>> Get()
        {
            throw new NotImplementedException();
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
    }
}