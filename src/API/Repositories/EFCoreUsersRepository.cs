using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Persistence;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EFCoreUsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EFCoreUsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(User obj)
    {
        await _dbContext.Users.AddAsync(obj);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string username) => await _dbContext.Users.AnyAsync(user => user.Username == username);

    public async Task<User> GetAsync(string username) => await _dbContext.Users.SingleAsync(user => user.Username == username);
}
