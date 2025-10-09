using StorageApp.User.Domain.Enum;

namespace StorageApp.User.Application.DTO
{
    public class CreateUserDTO :RegisterUserDTO
    {
        public List<RoleType> Role { get; set; }
    }
}
