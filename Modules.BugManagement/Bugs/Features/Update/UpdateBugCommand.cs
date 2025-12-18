using MediatR;
using Microsoft.Extensions.Logging;
using Modules.BugManagement.Bugs.ViewModels;
using Modules.BugManagement.Shared.Data.Repositories;
using Modules.BugManagement.Shared.Domain.Enums;
using Shared.Common.Models;
using Shared.Common.Wrappers;

namespace Modules.BugManagement.Bugs.Features.Update
{
    public record UpdateBugCommand(
        int Id,
        string Title,
        string Description,
        BugStatus Status,
        string Priority
    ) : IRequestWrapper<BugView>;

    public class UpdateBugCommandHandler(
        BugRepository repository)
        : IHandlerWrapper<UpdateBugCommand, BugView>
    {
        public async Task<Response<BugView>> Handle(UpdateBugCommand request, CancellationToken ct)
        {
            var bug = await repository.GetByIdAsync(request.Id, ct);

            if (bug == null)
                return Response.Fail<BugView>("Bug not found");

            bug.Title = request.Title;
            bug.Description = request.Description;
            bug.Status = request.Status;
            bug.Priority = request.Priority;

            await repository.UpdateAsync(bug, ct);

            return Response.Ok(bug.ToView());
        }
    }
}
