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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
