using Microsoft.Extensions.DependencyInjection;
using MovieFinder.Business.Services;
using MovieFinder.Business.Services.Interfaces;

namespace MovieFinder.Api.Setup
{
    public static class DependencyInjectionSetup
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMovieService, MovieService>()
                .AddTransient<IFormService, FormService>();
        }
    }
}
