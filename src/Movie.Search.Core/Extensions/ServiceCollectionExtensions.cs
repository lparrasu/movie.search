using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Search.Core.Abstractions.Caching;
using Movie.Search.Core.Behaviors;
using Movie.Search.Core.Features.Movies;
using System.Reflection;

namespace Movie.Search.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
       
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddEasyCaching(options => { options.UseInMemory(config, "mem"); });

            return services;
        }
    }
}
