using Microsoft.EntityFrameworkCore;
using StorageApp.User.Domain.Contracts;
using StorageApp.User.Domain.Entity;
using StorageApp.User.Infrastructure.Data;

namespace StorageApp.User.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {

        private readonly DbSet<UserModel> _dbContext = context.Set<UserModel>();

        public async Task<UserModel> Create(UserModel entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
            return entity;
        }

        public void DeleteById(string id, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Find(id);
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<UserModel>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbContext.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<UserModel> GetById(string id,CancellationToken cancellationToken)
        {
            return await _dbContext.FirstOrDefaultAsync(e => e.Id == id) ?? throw new Exception();
        }

        public async Task<UserModel?> GetByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.FirstOrDefaultAsync(b => b.Email != null && b.Email == email, cancellationToken);
        }
        public async Task<UserModel?> GetByName(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.FirstOrDefaultAsync(
                b => b.UserName != null && b.UserName == email, cancellationToken);
        }

        //Update function is straight in CommitAsync in UnitOfWork
    }
}
