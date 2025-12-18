using FluentResults;
using FluentValidation.Results;

namespace Application.Errors;

public class InputValidationErrror : Error
{
    public ValidationFailure ValidationFailure { get; }

    public InputValidationErrror(ValidationFailure validationFailure)
    {
        ValidationFailure = validationFailure;
    }
}
