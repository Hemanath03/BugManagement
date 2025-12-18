using FluentValidation.Results;

namespace Shared.Common.Models
{
    public static class ValidationFailureMapper
    {
        public static ValidationFailureResponse ToResponse(
            this List<ValidationFailure> failures)
        {
            return new ValidationFailureResponse(
                failures.Select(f => new ValidationError
                {
                    Code = f.ErrorCode,
                    Message = f.ErrorMessage
                })
            );
        }
    }
}
