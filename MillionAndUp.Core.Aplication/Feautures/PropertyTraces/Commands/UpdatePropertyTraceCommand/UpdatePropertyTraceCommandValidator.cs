using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Commands.UpdatePropertyTraceCommand
{
    public class UpdatePropertyTraceCommandValidator : AbstractValidator<UpdatePropertyTraceCommand>
    {
        public UpdatePropertyTraceCommandValidator()
        {
            RuleFor(p => p.DateSale)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .NotNull().WithMessage("{PropertyName} no debe ser nulo."); ;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Value)
               .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.Tax)
               .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor 0.")
              .NotNull().WithMessage("{PropertyName} no debe ser nulo.");
        }
    }
}
