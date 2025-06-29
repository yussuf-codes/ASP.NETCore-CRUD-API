using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Repositories.IRepositories;

namespace API.Repositories;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly List<User> _users = new()
    {
        // Password = 56482194
        // new User() { Id = Guid.Parse("1b70ad0d-1db7-477e-ae3d-135887bb2841"), Username = "joe", Salt = "ckk", Hash = "fbc5b562a851389c952a260b3635f4e8c186dc1758a098ed07ea86a572d1a4d5" },
        // Password = 54263325
        // new User() { Id = Guid.Parse("0419d07d-ce83-4521-ab10-fda5605b32e2"), Username = "lana", Salt = "ogn", Hash = "b3c38acadc18569d2949617b58808debafcf6dd2eb6da211e73fe1bdbc291836" }
    };

    public void Create(User obj) => _users.Add(obj);

    public bool Exists(string username) => _users.Any(user => user.Username == username);

    public User? Get(string username) => _users.SingleOrDefault(user => user.Username == username);
}
