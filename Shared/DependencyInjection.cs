using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Filters;

namespace Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ValidatorFilter<>));

            return services;
        }
    }
}
