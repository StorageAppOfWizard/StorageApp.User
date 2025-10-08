using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>()
                .Property(e => e.Role).HasConversion<string>();
        }
    }
}
