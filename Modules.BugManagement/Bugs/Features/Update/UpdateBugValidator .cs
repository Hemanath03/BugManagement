using FluentValidation;

namespace Modules.BugManagement.Bugs.Features.Update
{
    public class UpdateBugValidator : AbstractValidator<UpdateBugRequest>
    {
        public UpdateBugValidator()
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
