using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace MillionAndUp.Core.Application.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException() : base("Error(es) de validacion!")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ValidationException (IEnumerable<ValidationFailure> failures) : this()
        {
            foreach(var fail in failures)
            {
                Errors.Add(fail.ErrorMessage);
            }
        }

    }
}
