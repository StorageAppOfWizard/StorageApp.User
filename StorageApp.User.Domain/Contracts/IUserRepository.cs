using StorageApp.User.Domain.Entity;

namespace StorageApp.User.Domain.Contracts
{
    public interface IUserRepository
    {
        public Task<UserModel> GetById(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<UserModel>> GetAll(CancellationToken cancellationToken = default);
        public Task<UserModel> Create(UserModel entity, CancellationToken cancellationToken = default);
        public void DeleteById(Guid id, CancellationToken cancellationToken = default);
        public Task<UserModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    }
}
