using FluentValidation;
using MillionAndUp.Core.Application.Feautures.Users.Commands.AuthenticarUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(p => p.Email)
              .NotEmpty().WithMessage("{PropertyName} vacío.")
              .MaximumLength(80).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.")
              .EmailAddress().WithMessage("{PropertyName} de ser una dirección válida.");

            RuleFor(p => p.Password)
             .NotEmpty().WithMessage("Password vacío.");
        }
    }
}
