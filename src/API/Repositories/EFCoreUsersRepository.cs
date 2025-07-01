using System.Linq;
using API.Models;
using API.Persistence;
using API.Repositories.IRepositories;

namespace API.Repositories;

public class EFCoreUsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EFCoreUsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(User obj)
    {
        _dbContext.Users.Add(obj);
        _dbContext.SaveChanges();
    }

    public bool Exists(string username) => _dbContext.Users.Any(user => user.Username == username);

    public User? Get(string username) => _dbContext.Users.SingleOrDefault(user => user.Username == username);
}
