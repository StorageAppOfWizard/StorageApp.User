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
            };
        }

        public static UserModel ToEntity(this CreateUserDTO createUserDTO, string hashPassword)
        {
            return new UserModel
            {
                UserName = createUserDTO.UserName,
                Email = createUserDTO.Email,
                PasswordHash = hashPassword,
            };
        }

        public static void ToEntity(this UpdateUserDTO dto, UserModel user, string hashPassword)
        {
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PasswordHash = hashPassword;
        }
    }
}
