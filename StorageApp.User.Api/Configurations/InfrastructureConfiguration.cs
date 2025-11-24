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


            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
