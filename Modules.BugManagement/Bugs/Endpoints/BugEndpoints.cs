using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.BugManagement.Bugs.Features.Create;
using Modules.BugManagement.Bugs.Features.Delete;
using Modules.BugManagement.Bugs.Features.GetAll;
using Modules.BugManagement.Bugs.Features.GetById;
using Modules.BugManagement.Bugs.Features.Update;
using Modules.BugManagement.Bugs.ViewModels;
using Modules.BugManagement.Shared.RouterModules;
using Shared.Common.Models;
using Shared.Filters;

namespace Modules.BugManagement.Bugs.Controllers
{
    public class BugsEndpoints : BugModule
    {
        public override void AddEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost(string.Empty, Create)
                 .AddEndpointFilter<ValidatorFilter<CreateBugCommand>>()
                .Produces<BugView>();

            app.MapPut("{id}", Update)
                 .AddEndpointFilter<ValidatorFilter<UpdateBugRequest>>()
                .Produces<BugView>();

            app.MapDelete("{id}", Delete)
                .Produces<string>();

            app.MapGet("{id}", GetById)
                .Produces<BugView>();

            app.MapPost("pagination", GetAll)
                .Produces<PaginatedList<BugView>>();
        }

        public static async Task<IResult> Create(
            IMediator mediator,
            CreateBugCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }

        public static async Task<IResult> Update(
            IMediator mediator,
            int id,
            UpdateBugRequest reqeuest,
            CancellationToken cancellationToken)
        {
            var command = new UpdateBugCommand(
                id,
                reqeuest.Title,
                reqeuest.Description,
                reqeuest.Status,
                reqeuest.Priority);
            var response = await mediator.Send(command, cancellationToken);
            return Results.Ok(response);
        }

        public static async Task<IResult> Delete(
            IMediator mediator,
            int id,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new DeleteBugCommand(id), cancellationToken);
            return Results.Ok(response);
        }

        public static async Task<IResult> GetById(
            IMediator mediator,
            int id,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetBugByIdQuery(id), cancellationToken);
            return Results.Ok(response);
        }

        public static async Task<IResult> GetAll(
            IMediator mediator, SearchRequest request,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetBugPaginationQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Search = request.Search,
            }, cancellationToken);
            return Results.Ok(response);
        }
    }
}
