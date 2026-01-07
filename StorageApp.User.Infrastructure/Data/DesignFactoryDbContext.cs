using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StorageApp.User.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

<<<<<<< HEAD
            optionsBuilder.UseNpgsql("User ID=root;Password=Lagavi30!;Host=localhost;Port=5433;Database=users;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;");
=======
            optionsBuilder.UseSqlServer("Server=sqlserverdbuser;Database=users;User Id=sa;Password=Lagavi30!;TrustServerCertificate=True;");
>>>>>>> a75fd94f42e3a66b4ad95ab8af014bc1c1284a9d

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
