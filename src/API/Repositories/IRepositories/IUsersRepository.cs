using System.Threading.Tasks;
using API.Models;

namespace API.Repositories.IRepositories;

public interface IUsersRepository
{
    Task CreateAsync(User obj);
    Task<bool> ExistsAsync(string username);
    Task<User> GetAsync(string username);
}
