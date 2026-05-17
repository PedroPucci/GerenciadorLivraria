using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Domain.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }        
        public BookGenre Genre { get; set; }
        public string? ISBN { get; set; }
        public string? Publisher { get; set; }
        public int PublishedYear { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModificationDate { get; set; }
    }
}