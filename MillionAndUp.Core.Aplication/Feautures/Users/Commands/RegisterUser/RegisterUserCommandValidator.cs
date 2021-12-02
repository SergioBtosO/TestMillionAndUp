using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} vacío.")
               .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.LastName)
              .NotEmpty().WithMessage("LastName vacío.")
              .MaximumLength(80).WithMessage("LastName no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Email)
              .NotEmpty().WithMessage("{PropertyName} vacío.")
              .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.")
              .EmailAddress().WithMessage("{PropertyName} de ser una dirección válida.");

            RuleFor(p => p.UserName)
               .NotEmpty().WithMessage("UserName vacío.")
               .MaximumLength(80).WithMessage("UserName no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("Password vacío.")
               .MaximumLength(20).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.")
               .MinimumLength(8).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.ConfirmPassword)
                .Equal(p =>p.Password).WithMessage("Password y ConfirmaPassword diferentes.");

        }
    }
}
