using FluentValidation;

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Commands.CreatePropertyImageCommand
{
    public class CreatePropertyImageCommandValidator : AbstractValidator<CreatePropertyImageCommand>
    {
        public CreatePropertyImageCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres.");

            RuleFor(p => p.File)
               .NotNull().WithMessage("{PropertyName} no debe ser nulo.");

            RuleFor(p => p.IdProperty)
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor 0.")
              .NotNull().WithMessage("{PropertyName} no debe ser nulo.");
        }
    }
}
