using MediatR;
using Microsoft.Extensions.Logging;
using Modules.BugManagement.Bugs.ViewModels;
using Modules.BugManagement.Shared.Data.Repositories;
using Shared.Common.Models;
using Shared.Common.Wrappers;

namespace Modules.BugManagement.Bugs.Features.GetAll
{
    public record GetBugPaginationQuery() : SearchRequest, IRequestWrapper<PaginatedList<BugView>>;

    public class GetBugPaginationQueryHandler(
        BugRepository repository)
        : IHandlerWrapper<GetBugPaginationQuery, PaginatedList<BugView>>
    {
        public async Task<Response<PaginatedList<BugView>>> Handle(GetBugPaginationQuery request, CancellationToken ct)
        {
            var pagedBugs = await repository.GetPagedAsync(request, ct);

            var views = pagedBugs.Items
                .Select(b => b.ToView())
                .ToList();

            var result = new PaginatedList<BugView>(
                views,
                pagedBugs.TotalCount,
                pagedBugs.PageNumber,
                request.PageSize
            );

            return Response.Ok(result);
        }
    }
}
