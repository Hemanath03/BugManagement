namespace Shared.Common.Models
{
    public class ValidationFailureResponse
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationFailureResponse(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
