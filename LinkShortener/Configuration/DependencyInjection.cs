using LinkShortener.Data;
using LinkShortener.Service;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDb(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContextPool<MariaDbContext>(options => options
            .UseMySql(
                config.GetConnectionString("MariaDbConnectionString"),
                new MariaDbServerVersion(new Version(10, 3, 39))
                )
            );

            return service;
        }

        public static IServiceCollection CommonServices(this IServiceCollection service)
        {
            service.AddScoped<ILinkService, LinkService>();

            service.AddControllersWithViews();

            return service;
        }
    }
}
