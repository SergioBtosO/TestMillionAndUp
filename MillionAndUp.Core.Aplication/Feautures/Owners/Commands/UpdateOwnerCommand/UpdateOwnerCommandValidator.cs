using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Commands.UpdateOwnerCommand
{
    public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
    {
        public UpdateOwnerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Birthday)
                .NotEmpty().WithMessage("{PropertyName} vacío.");
        }

    }
}
