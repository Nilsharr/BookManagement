using BookManagement.Shared.Dto;
using FluentValidation;

namespace BookManagement.RestServer.Validators;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(128).WithMessage("Author name must not exceed 128 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(256).WithMessage("Title must not exceed 256 characters.");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .MaximumLength(128).WithMessage("Genre must not exceed 128 characters.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.")
            .MaximumLength(256).WithMessage("Publisher must not exceed 256 characters.");

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("Language is required.")
            .MaximumLength(128).WithMessage("Language must not exceed 128 characters.");

        RuleFor(x => x.PublicationDate)
            .NotEmpty().WithMessage("Publication date is required.");
    }
}