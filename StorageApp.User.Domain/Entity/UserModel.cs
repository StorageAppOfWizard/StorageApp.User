using Microsoft.AspNetCore.Identity;
using StorageApp.User.Domain.Enum;

namespace StorageApp.User.Domain.Entity
{
    public class UserModel :IdentityUser
    {
        public string UserName { get; set; }
        public List<RoleType> Role { get; set; } = new List<RoleType>() { RoleType.Member };
    }
}
