using Dicenamics.Models;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Data;
public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options) : base(options) { }
    
    public DbSet<Modificador> Modificadores {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
