namespace StorageApp.User.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public async Task CommitAsync() { }

       
    }
}
