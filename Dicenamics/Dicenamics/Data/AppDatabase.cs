using Dicenamics.Models;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Data;
public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
    {
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<AcessoUsuarioDados> AcessosUsuariosDados { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<DadoCompostoSala> DadosCompostosSalas { get; set; }
    public DbSet<DadoSimplesSala> DadosSimplesSalas { get; set; }
    public DbSet<DadoSimples> DadosSimples { get; set; }
    public DbSet<DadoComposto> DadosCompostos { get; set; }
    public DbSet<ModificadorFixo> ModificadoresFixos {get; set;}
    public DbSet<ModificadorVariavel> ModificadoresVariaveis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
