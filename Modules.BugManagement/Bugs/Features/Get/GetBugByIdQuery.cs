using MediatR;
using Microsoft.Extensions.Logging;
using Modules.BugManagement.Bugs.ViewModels;
using Modules.BugManagement.Shared.Data.Repositories;
using Shared.Common.Models;
using Shared.Common.Wrappers;

namespace Modules.BugManagement.Bugs.Features.GetById
{
    public record GetBugByIdQuery(int Id) : IRequestWrapper<BugView>;

    public class GetBugByIdQueryHandler(
        BugRepository repository)
        : IHandlerWrapper<GetBugByIdQuery, BugView>
    {
        public async Task<Response<BugView>> Handle(GetBugByIdQuery request, CancellationToken ct)
        {
            var bug = await repository.GetByIdAsync(request.Id, ct);

            if (bug == null)
                return Response.Fail<BugView>("Bug not found");

            return Response.Ok(bug.ToView());
        }
    }
}
