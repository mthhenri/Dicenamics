using Dicenamics.Models;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Data;
public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
    {
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ModificadorFixo> ModificadoresFixos {get; set;}
    public DbSet<DadoSimples> DadosSimples { get; set; }
    public DbSet<DadoBasico> DadosBasicos { get; set; }
    public DbSet<Sala> Salas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
