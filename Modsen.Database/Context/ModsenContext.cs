using Microsoft.EntityFrameworkCore;
using Modsen.Database.Context.Configurations;
using Modsen.Domain.Models;

namespace Modsen.Database.Context;

public class ModsenContext : DbContext
{
    public ModsenContext(DbContextOptions<ModsenContext> options) : base(options) => Database.EnsureCreated();

    public DbSet<BookModel> Books { get; set; }

    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}