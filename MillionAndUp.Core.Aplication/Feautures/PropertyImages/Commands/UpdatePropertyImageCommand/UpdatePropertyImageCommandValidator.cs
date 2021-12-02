using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.UpdatePropertyImageCommand
{
    public class UpdatePropertyImageCommandValidator: AbstractValidator<UpdatePropertyImageCommand>
    {
        public UpdatePropertyImageCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor 0.")
              .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.File)
               .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

           
        }
    }
}
