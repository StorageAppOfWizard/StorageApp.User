using StorageApp.User.Application.Contracts;

namespace StorageApp.User.Application.Security
{
    public class HasherPassword : IHasherPassword
    {
        private readonly int WORKER = 10;

        public string Hasher(string password) => BCrypt.Net.BCrypt.HashPassword(password, WORKER);

        public bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);



        public string VerifyUpdatePassword(string password, string hash)
        {
            if (BCrypt.Net.BCrypt.Verify(password, hash))
            {
                if (BCrypt.Net.BCrypt.PasswordNeedsRehash(hash, WORKER))
                {
                    var newHash = BCrypt.Net.BCrypt.HashPassword(password);
                    return newHash;
                }   
            }
            return hash;
        }
    }
}
