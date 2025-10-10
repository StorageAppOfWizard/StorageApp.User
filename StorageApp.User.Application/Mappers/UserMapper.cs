using StorageApp.User.Application.DTO;
using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this UserModel user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public static UserModel ToEntity(this CreateUserDTO dto, string hashPassword)
        {
            return new UserModel
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = hashPassword,
                Role = dto.Role
            };
        }

        public static void ToEntity(this UpdateUserDTO dto, UserModel user, string hashPassword)
        {
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.PasswordHash = hashPassword;
        }
        public static UserModel ToEntity(this RegisterUserDTO dto, UserModel user, string hashPassword)
        {
            return new UserModel
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = hashPassword,
            };
        }
        public static UserModel ToEntity(this LoginUserDTO dto)
        {
            return new UserModel
            {
                Email = dto.Email,
                PasswordHash = dto.Password,
            };
        }


    }
}
