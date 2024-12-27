using Catalog.API.Repositories;

namespace Catalog.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
