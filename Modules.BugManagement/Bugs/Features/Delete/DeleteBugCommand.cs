using MediatR;
using Microsoft.Extensions.Logging;
using Modules.BugManagement.Shared.Data.Repositories;
using Shared.Common.Models;
using Shared.Common.Wrappers;

namespace Modules.BugManagement.Bugs.Features.Delete
{
    public record DeleteBugCommand(int Id) : IRequestWrapper<bool>;

    public class DeleteBugCommandHandler(
        BugRepository repository,
        ILogger<DeleteBugCommandHandler> logger)
                : IHandlerWrapper<DeleteBugCommand, bool>
    {
        public async Task<Response<bool>> Handle(DeleteBugCommand request, CancellationToken ct)
        {
            logger.LogWarning("Deleting bug {BugId}", request.Id);

            var bug = await repository.GetByIdAsync(request.Id, ct);
            if (bug == null)
            {
                return Response.Fail<bool>("Bug not found");
            }                       

            await repository.DeleteAsync(bug, ct);

            return Response.Ok(true);
        }
    }
}
