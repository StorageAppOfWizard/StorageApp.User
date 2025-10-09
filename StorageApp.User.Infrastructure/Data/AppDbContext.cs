using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorageApp.User.Domain.Entity;
using StorageApp.User.Domain.Enum;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };

            builder.Entity<UserModel>()
                .Property(u => u.Role)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, options),
                    v => JsonSerializer.Deserialize<List<RoleType>>(v, options) ?? new List<RoleType>()
                );

        }
    }
}
