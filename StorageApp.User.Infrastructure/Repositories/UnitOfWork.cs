using StorageApp.User.Domain.Contracts;
using StorageApp.User.Infrastructure.Data;

namespace StorageApp.User.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository =>_userRepository ??= new UserRepository(_appDbContext);

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task CommitAsync() => await _appDbContext.SaveChangesAsync();
        
    }
}
