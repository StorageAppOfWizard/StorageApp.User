using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Contracts
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
    }
}
