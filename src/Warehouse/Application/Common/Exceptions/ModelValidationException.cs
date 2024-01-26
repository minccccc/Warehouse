using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ModelValidationException : Exception
{
    public List<ValidationFailure> Errors { get; set; }

    public ModelValidationException(List<ValidationFailure> errors)
        : base(string.Join(',', errors.Select(f => f.ErrorMessage)))
    {
        Errors = errors;
    }
}