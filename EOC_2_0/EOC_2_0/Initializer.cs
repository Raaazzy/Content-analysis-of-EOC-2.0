using Automarket.Service.Implementations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Repository;
using EOC_2_0.Service.Interfaces;

namespace EOC_2_0
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Verb>, VerbRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IVerbService, VerbService>();
        }
    }
}
