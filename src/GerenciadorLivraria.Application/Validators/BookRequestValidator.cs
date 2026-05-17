using FluentValidation;
using GerenciadorLivraria.Application.Contracts.Dto.BookDto;

namespace GerenciadorLivraria.Application.Validators
{
    public class BookRequestValidator : AbstractValidator<CreateBookRequestDto>
    {
        public BookRequestValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage("Title can not be null or empty.")
                .MinimumLength(5)
                    .WithMessage("Title must have at least 5 characters.");

            RuleFor(p => p.Author)
                .NotEmpty()
                    .WithMessage("Author can not be null or empty.")
                .MinimumLength(5)
                    .WithMessage("Author must have at least 5 characters.");

            RuleFor(p => p.Publisher)
                .NotEmpty()
                    .WithMessage("Publisher can not be null or empty.")
                .MinimumLength(5)
                    .WithMessage("Publisher must have at least 5 characters.");

            RuleFor(p => p.Description)
                .NotEmpty()
                    .WithMessage("Description can not be null or empty.")
                .MinimumLength(20)
                    .WithMessage("Description must have at least 20 characters.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price can not be negative.");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Stock can not be negative.");

            RuleFor(p => p.ISBN)
                .NotEmpty()
                    .WithMessage("ISBN can not be null or empty.")
                .Matches(@"^\d{13}$")
                    .WithMessage("ISBN must contain exactly 13 numeric digits.");

            RuleFor(p => p.PublishedYear)
                .InclusiveBetween(1000, DateTime.UtcNow.Year)
                    .WithMessage("Published year is invalid.");

            RuleFor(p => p.Genre)
                .IsInEnum()
                    .WithMessage("Invalid genre.");
        }
    }
}