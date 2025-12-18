using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Shared.EndpointRouter;

namespace Modules.BugManagement.Shared.RouterModules
{
    public abstract class BugModule : EndpointRoute
    {
        public override IEndpointRouteBuilder OnConfiguring(IEndpointRouteBuilder app)
        {
            return app.MapGroup("/bugs");
        }
    }
}
