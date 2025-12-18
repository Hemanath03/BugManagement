using FluentValidation;

namespace Modules.BugManagement.Bugs.Features.Create
{
    public class CreateBugValidator : AbstractValidator<CreateBugCommand>
    {
        public CreateBugValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required");
        }
    }
}
