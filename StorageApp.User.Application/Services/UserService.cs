using Ardalis.Result;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Application.DTO;
using StorageApp.User.Application.Extension;
using StorageApp.User.Application.Mappers;
using StorageApp.User.Application.Validators;
using StorageApp.User.Domain.Contracts;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHasherPassword _hasherPassword;

        public UserService(IUnitOfWork unitOfWork, IHasherPassword hasherPassword)
        {
            _unitOfWork = unitOfWork;
            _hasherPassword = hasherPassword;
        }

        public async Task<Result<List<UserDTO>>> GetAllAsync()
        {
            var entity = await _unitOfWork.UserRepository.GetAll();
            if (entity is null)
                return Result.NotFound("No users found");

            return Result.Success(entity.Select(u => u.ToDTO()).ToList());
        }

        public async Task<Result<UserDTO>> GetByIdAsync(string id)
        {
            if (id == string.Empty)
                return Result.Error("Invalid user ID provided.");

            var entity = await _unitOfWork.UserRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("User not found");

            return Result.Success(entity.ToDTO());

        }
        public async Task<Result<UserModel>> CreateAsync(CreateUserDTO dto)
        {
            var validation = dto.ToValidateErrors(new UserValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingUser = await _unitOfWork.UserRepository.GetByName(dto.UserName);
            if (existingUser != null)
                return Result.Conflict($"User with the name '{existingUser.UserName}' already exists.");

            var passwordHash = _hasherPassword.Hasher(dto.Password);
            var entity = dto.ToEntity(passwordHash);

            await _unitOfWork.UserRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.Created(entity);
        }

        public async Task<Result> UpdateAsync(UpdateUserDTO dto)
        {
            //var validation = dto.ToValidateErrors(new UserValidator());
            //if (validation.Count != 0)
            //    return Result.Invalid(validation);

            var existingUser = await _unitOfWork.UserRepository.GetByName(dto.UserName);
            if (existingUser != null)
                return Result.Conflict($"User with the name '{existingUser.UserName}' already exists.");

            var entity = await _unitOfWork.UserRepository.GetById(dto.Id);
            if (entity is null)
                return Result.NotFound("User Not Found");

            var passwordVerified = _hasherPassword.VerifyUpdatePassword(dto.Password, entity.PasswordHash);

            dto.ToEntity(entity, passwordVerified);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("User Updated");
        }

        public async Task<Result> DeleteAsync(string id)
        {

            var entity = await _unitOfWork.UserRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("Invalid User ID provided.");

            _unitOfWork.UserRepository.DeleteById(id);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("User deleted successfully.");
        }


        public void Dispose() => _unitOfWork.Dispose();



    }
}

