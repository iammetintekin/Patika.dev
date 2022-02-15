using System;
using FluentValidation;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookValidator()
        {
            RuleFor(s => s.Id).NotEmpty().GreaterThan(0).WithMessage("Id sıfırdan büyük olmalı");

        }
    }
}
