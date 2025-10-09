using Ardalis.Result;
using StorageApp.User.Application.DTO;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Contracts
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterUserDTO dto);
        Task<Result<UserModel>> LoginAsync(LoginUserDTO dto);
    }
}
