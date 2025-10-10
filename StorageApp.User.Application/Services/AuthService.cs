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
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHasherPassword _hasherPassword;


        public AuthService(IUnitOfWork unitOfWork, IHasherPassword hasherPassword)
        {
            _unitOfWork = unitOfWork;
            _hasherPassword = hasherPassword;
        }

        public async Task<Result<UserModel>> LoginAsync(LoginUserDTO dto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetByEmail(dto.Email);

            if (!ValidateUser(existingUser, dto))
                return Result.Unauthorized();

            return Result.Success(existingUser); 
        }

        public async Task<Result> RegisterAsync(RegisterUserDTO dto)
        {
            var validation = dto.ToValidateErrors(new UserRegisterValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingUser = await _unitOfWork.UserRepository.GetByName(dto.UserName);
            if (existingUser != null)
                return Result.Conflict($"User with the name '{existingUser.UserName}' already exists.");

            var passwordHash = _hasherPassword.Hasher(dto.Password);
            var entity = dto.ToEntity(existingUser, passwordHash);

            await _unitOfWork.UserRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("User Created");
        }

        public bool ValidateUser(UserModel user, LoginUserDTO dto)
        {
            if (user is null) return false;


            if (! _hasherPassword.VerifyPassword(dto.Password, user.PasswordHash))
                return false;

            return true;
        }


    }
}
