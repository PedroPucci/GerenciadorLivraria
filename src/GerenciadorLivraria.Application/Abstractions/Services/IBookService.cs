using GerenciadorLivraria.Application.Contracts.Dto.BookDto;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.OperationResult;

namespace GerenciadorLivraria.Application.Abstractions.Services
{
    public interface IBookService
    {
        Task<Result<BookEntity>> Add(CreateBookRequestDto createBookRequestDto);
        Task<Result<bool>> Update(string id, UpdateBookRequestDto updateBookRequestDto);
        Task<Result<bool>> Delete(string id);
        Task<List<BookEntity>> Get();
        Task<Result<BookResponseDto>> GetById(string id);
        Task<Result<BookResponseDto>> GetByName(string name);
    }
}