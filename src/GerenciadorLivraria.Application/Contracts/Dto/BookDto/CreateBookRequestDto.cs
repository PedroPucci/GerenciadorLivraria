using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Application.Contracts.Dto.BookDto
{
    public class CreateBookRequestDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public BookGenre Genre { get; set; }
        public string? ISBN { get; set; }
        public string? Publisher { get; set; }
        public int PublishedYear { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}