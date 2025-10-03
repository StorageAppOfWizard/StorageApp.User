namespace StorageApp.User.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public async Task CommitAsync() { }
       
    }
}
