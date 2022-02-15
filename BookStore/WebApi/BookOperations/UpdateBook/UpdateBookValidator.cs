using FluentValidation;
using System;


namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0); //book Id
            RuleFor(c => c.Model.GenreId).GreaterThan(0).NotEmpty();
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(3);
        }
    }
}
