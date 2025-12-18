using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Common.Models;

namespace Shared.Filters
{
    public class ValidatorFilter<T> : IEndpointFilter
    {
        private readonly IValidator<T> _validator;

        public ValidatorFilter(IValidator<T> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // Find the command/query in endpoint arguments
            T? validatable = context.Arguments
                .OfType<T>()
                .FirstOrDefault();

            if (validatable is null)
            {
                return Results.BadRequest(new
                {
                    message = "Invalid request payload"
                });
            }

            var validationResult = await _validator.ValidateAsync(validatable);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse()
                );
            }

            // Continue to handler if valid
            return await next(context);
        }
    }
}
