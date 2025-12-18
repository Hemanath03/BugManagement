using Modules.BugManagement.Shared.Domain.Enums;

namespace Modules.BugManagement.Bugs.Features.Update
{
    public record UpdateBugRequest(string Title,
        string Description,
        BugStatus Status,
        string Priority);
   
}
