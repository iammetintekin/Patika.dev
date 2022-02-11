using FluentValidation;
using System;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0); //book Id
        }
    }
}
