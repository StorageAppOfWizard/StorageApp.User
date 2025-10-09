namespace StorageApp.User.Application.Contracts
{
    public interface IHasherPassword
    {
        string Hasher(string password);
        bool VerifyPassword(string password, string hash);
        string VerifyUpdatePassword(string password, string hash);
    }
}
