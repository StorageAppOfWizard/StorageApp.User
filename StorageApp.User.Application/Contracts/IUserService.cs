using Ardalis.Result;
using StorageApp.User.Application.DTO;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Contracts
{
    public interface IUserService
    {
        public Task<Result<List<UserDTO>>> GetAllAsync();
        public Task<Result<UserDTO>> GetByIdAsync(string id);
        public Task<Result<UserModel>> CreateAsync(CreateUserDTO createUserDTO);
        public Task<Result> UpdateAsync(UpdateUserDTO updateUserDTO);
        public Task<Result> DeleteAsync(string id);
    }
}
