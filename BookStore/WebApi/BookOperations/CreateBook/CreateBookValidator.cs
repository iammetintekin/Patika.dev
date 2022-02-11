using FluentValidation;
using System;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(c => c.Model.GenreId).GreaterThan(0);
            RuleFor(c=>c.Model.PageCount).GreaterThan(0);
            RuleFor(c => c.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(3);
        }
    }
}
