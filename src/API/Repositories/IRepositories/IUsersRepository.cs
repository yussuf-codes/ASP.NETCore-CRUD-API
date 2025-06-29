using API.Models;

namespace API.Repositories.IRepositories;

public interface IUsersRepository
{
    void Create(User obj);
    bool Exists(string username);
    User? Get(string username);
}
