using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Properties.Commands.UpdatePropertyCommand
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.CodeInternal)
               .NotEmpty().WithMessage("Code Internal vacío.")
               .MaximumLength(30).WithMessage("Code Internal no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} debe ser mayor o igual a 0.")
               .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.Year)
              .GreaterThanOrEqualTo(1800).WithMessage("{PropertyName} debe ser mayor o igual a 1800.")
              .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.IdOwner)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor 0.")
              .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

        }
    }
}
