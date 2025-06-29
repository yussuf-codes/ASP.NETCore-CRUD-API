using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
