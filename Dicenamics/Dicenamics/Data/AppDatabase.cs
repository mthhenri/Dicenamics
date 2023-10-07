using Dicenamics.Models;
using Microsoft.EntityFrameworkCore;

namespace Dicenamics.Data;
public class AppDatabase : DbContext
{
    public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
    {
    }
    
    public DbSet<Usuario>? Usuarios { get; set; }
    public DbSet<Sala>? Salas { get; set; }
    public DbSet<DadoCompostoSala>? DadosCompostosSalas { get; set; }
    public DbSet<DadoSimplesSala>? DadosSimplesSalas { get; set; }
    public DbSet<DadoSimples>? DadosSimples { get; set; }
    public DbSet<DadoComposto>? DadosCompostos { get; set; }
    public DbSet<ModificadorFixo>? ModificadoresFixos {get; set;}
    public DbSet<ModificadorVariavel>? ModificadoresVariaveis { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DadoSimples>()
            .ToTable("DadosSimples");
        
        modelBuilder.Entity<DadoSimples>()
            .HasOne(ds => ds.ModificadorVariavel)
            .WithOne(mv => mv.Dado)
            .HasForeignKey<ModificadorVariavel>(mv => mv.DadoSimplesId); // Defina a chave estrangeira em ModificadorVariavel

        modelBuilder.Entity<DadoComposto>()
            .ToTable("DadosCompostos");        

        modelBuilder.Entity<DadoComposto>()
            .HasMany(dc => dc.Variaveis) // Propriedade de navegação em DadoComposto
            .WithOne(df => df.DadoComposto) // Propriedade de navegação correspondente em DadoCompostoModVar
            .HasForeignKey(df => df.DadoId); // Chave estrangeira em DadoCompostoModVar

        modelBuilder.Entity<DadoCompostoModFixo>()
            .HasKey(df => new {df.DadoId, df.ModificadorId});

        modelBuilder.Entity<DadoCompostoModFixo>()
            .HasOne(df => df.DadoComposto)
            .WithMany(df => df.Fixos)
            .HasForeignKey(df => df.ModificadorId);

        modelBuilder.Entity<DadoCompostoModFixo>()
            .HasOne(df => df.ModificadorFixo)
            .WithMany(df => df.Fixos)
            .HasForeignKey(df => df.DadoId);
        
        modelBuilder.Entity<DadoCompostoModVar>()
            .HasOne(df => df.DadoComposto)
            .WithMany(dc => dc.Variaveis)
            .HasForeignKey(df => df.DadoId);

        modelBuilder.Entity<DadoCompostoModVar>()
            .HasOne(df => df.ModificadorVariavel)
            .WithMany(mv => mv.Variaveis) 
            .HasForeignKey(df => df.ModificadorId);
        
        modelBuilder.Entity<SalaUsuario>()
            .HasKey(us => us.SalaUsuarioId);

        modelBuilder.Entity<SalaUsuario>()
            .HasOne(us => us.Usuario)
            .WithMany(u => u.Salas)
            .HasForeignKey(us => us.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SalaUsuario>()
            .HasOne(us => us.Sala)
            .WithMany(s => s.Convidados)
            .HasForeignKey(us => us.SalaId);

        base.OnModelCreating(modelBuilder);
    }

}
