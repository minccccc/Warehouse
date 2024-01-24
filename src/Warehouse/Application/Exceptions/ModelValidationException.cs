using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ModelValidationException : Exception
    {
        public List<ValidationFailure> Errors{ get; set; }

        public ModelValidationException(ValidationResult validationResult)
        {
            Errors = validationResult.Errors;
        }
    }
}
