using MediatR;
using Microsoft.Extensions.Logging;
using Modules.BugManagement.Bugs.ViewModels;
using Modules.BugManagement.Shared.Data.Repositories;
using Modules.BugManagement.Shared.Domain.Entities;
using Modules.BugManagement.Shared.Domain.Enums;
using Shared.Common.Models;
using Shared.Common.Wrappers;

namespace Modules.BugManagement.Bugs.Features.Create
{
    public record CreateBugCommand(
        string Title,
        string Description,
        BugStatus Status,
        string Priority
    ) : IRequestWrapper<BugView>;

    public class CreateBugCommandHandler(
        BugRepository repository,
        ILogger<CreateBugCommandHandler> logger)
                : IHandlerWrapper<CreateBugCommand, BugView>
    {
        public async Task<Response<BugView>> Handle(CreateBugCommand request, CancellationToken ct)
        {
            logger.LogInformation("Creating bug: {Title}", request.Title);

            var bug = new Bug
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority
            };

            await repository.AddAsync(bug, ct);

            logger.LogInformation("Bug created with Id {BugId}", bug.Id);

            return Response.Ok (bug.ToView());
        }
    }
}
