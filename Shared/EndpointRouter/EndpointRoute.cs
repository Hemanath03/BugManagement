using Carter;
using Microsoft.AspNetCore.Routing;

namespace Shared.EndpointRouter
{
    public abstract class EndpointRoute : CarterModule
    {
        protected EndpointRoute() : base("api")
        {
        }

        public abstract void AddEndpoints(IEndpointRouteBuilder app);

        public virtual IEndpointRouteBuilder OnConfiguring(IEndpointRouteBuilder app)
        {
            return app;
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            AddEndpoints(OnConfiguring(app));
        }
    }
}
