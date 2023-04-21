using Microsoft.EntityFrameworkCore;
using Modsen.Database.Context.Configurations;
using Modsen.Domain.Models;

namespace Modsen.Database.Context;

public class ModsenContext : DbContext
{
    public ModsenContext()
    {
    }

    public ModsenContext(DbContextOptions<ModsenContext> options) : base(options)
    {
    }

    public DbSet<BookModel> Books { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = modsen.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}