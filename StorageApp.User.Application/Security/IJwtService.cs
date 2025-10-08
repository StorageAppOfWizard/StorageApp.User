using Microsoft.Extensions.Configuration;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Security
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
    }
}
