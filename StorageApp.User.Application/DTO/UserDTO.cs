using StorageApp.User.Domain.Enum;

namespace StorageApp.User.Application.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RoleType> Role { get; set; }


    }
}
