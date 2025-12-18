using FluentValidation;

namespace Modules.BugManagement.Bugs.Features.Delete
{
    public class DeleteBugValidator : AbstractValidator<DeleteBugCommand>
    {
        public DeleteBugValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Invalid bug id");
        }
    }
}
