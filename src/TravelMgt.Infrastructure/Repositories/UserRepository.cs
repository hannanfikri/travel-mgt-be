using Microsoft.EntityFrameworkCore;
using TravelMgt.Domain.Entities;
using TravelMgt.Domain.Interfaces;
using TravelMgt.Infrastructure.Persistence;

namespace TravelMgt.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly TravelMgtDbContext _dbContext;

    public UserRepository(TravelMgtDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
