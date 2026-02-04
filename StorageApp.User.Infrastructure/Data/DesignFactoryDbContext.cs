using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StorageApp.User.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();


            optionsBuilder.UseNpgsql("User ID=root;Password=Lagavi30!;Host=dbpostgresUser;Database=users;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;");


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
