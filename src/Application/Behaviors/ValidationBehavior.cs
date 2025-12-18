using Application.Errors;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(vr => !vr.IsValid)
            .SelectMany(vr => vr.Errors)
            .ToList();

        if (errors.Count == 0)
            return await next(cancellationToken);

        var result = new TResponse();
        result.Reasons.AddRange(errors.Select(e => new InputValidationErrror(e)));
        return result;
    }
}
