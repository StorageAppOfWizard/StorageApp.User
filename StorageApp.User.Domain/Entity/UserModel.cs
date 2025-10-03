using Microsoft.AspNetCore.Identity;

namespace StorageApp.User.Domain.Entity
{
    public class UserModel :IdentityUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
