using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Application.Contracts.Dto.BookDto;
using GerenciadorLivraria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWorkService _uow;

        public BookController(IUnitOfWorkService uow)
        {
            _uow = uow;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost()]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] CreateBookRequestDto createBookRequestDto)
        {
            var result = await _uow.BookService.Add(createBookRequestDto);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateBookRequestDto updateBookRequestDto)
        {
            var result = await _uow.BookService.Update(id, updateBookRequestDto );

            if (!result.Success)
                return NotFound(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _uow.BookService.Delete(id);

            if (!result.Success)
                return NotFound(result);

            return NoContent();
        }

        //[HttpGet("all")]
        //[ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _uow.BookService.Get();
        //    return Ok(result);
        //}

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _uow.BookService.Get(page, size);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _uow.BookService.GetById(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(BookEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var result = await _uow.BookService.GetByName(name);
            return Ok(result);
        }
    }
}