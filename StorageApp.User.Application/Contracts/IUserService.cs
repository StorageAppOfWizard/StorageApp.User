using Ardalis.Result;
using StorageApp.User.Application.DTO;

namespace StorageApp.User.Application.Contracts
{
    public interface IUserService
    {
        public Task<Result<List<UserDTO>>> GetAllAsync();
        public Task<Result<UserDTO>> GetByIdAsync(Guid id);
        public Task<Result> CreateAsync(CreateUserDTO createUserDTO);
        public Task<Result> UpdateAsync(UpdateUserDTO updateUserDTO);
        public Task<Result> DeleteAsync(Guid id);
    }
}
