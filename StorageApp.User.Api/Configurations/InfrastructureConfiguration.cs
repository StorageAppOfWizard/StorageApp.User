using Microsoft.EntityFrameworkCore;
using StorageApp.User.Domain.Contracts;
using StorageApp.User.Infrastructure.Data;
using StorageApp.User.Infrastructure.Repositories;

namespace StorageApp.User.Api.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
<<<<<<< HEAD
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")));
=======
            services.AddDbContext<AppDbContext>(dbOptions =>
                dbOptions.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnectionString"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                        );
                    }
                )
                .EnableDetailedErrors()
            );
>>>>>>> a75fd94f42e3a66b4ad95ab8af014bc1c1284a9d


            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
