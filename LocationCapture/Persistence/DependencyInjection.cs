using LocationCapture.Core;
using LocationCapture.Core.Repositories;
using LocationCapture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocationCapture.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPlacementRepository, PlacementRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;
        }
    }
}
