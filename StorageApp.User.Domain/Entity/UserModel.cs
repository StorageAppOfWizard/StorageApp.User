using Microsoft.AspNetCore.Identity;

namespace StorageApp.User.Domain.Entity
{
    public class UserModel :IdentityUser
    {
        public string FullName{ get; set; }
    }
}
