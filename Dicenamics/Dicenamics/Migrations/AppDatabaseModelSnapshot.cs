﻿// <auto-generated />
using System;
using Dicenamics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dicenamics.Migrations
{
    [DbContext(typeof(AppDatabase))]
    partial class AppDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("Dicenamics.Models.DadoComposto", b =>
                {
                    b.Property<int>("DadoCompostoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DadoCompostoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("DadosCompostos");
                });

            modelBuilder.Entity("Dicenamics.Models.DadoSimples", b =>
                {
                    b.Property<int>("DadoSimplesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Condicao")
                        .HasColumnType("TEXT");

                    b.Property<int>("Faces")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DadoSimplesId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("DadosSimples");
                });

            modelBuilder.Entity("Dicenamics.Models.ModificadorFixo", b =>
                {
                    b.Property<int>("ModificadorFixoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("Valor")
                        .HasColumnType("INTEGER");

                    b.HasKey("ModificadorFixoId");

                    b.ToTable("ModificadoresFixos");
                });

            modelBuilder.Entity("Dicenamics.Models.ModificadorVariavel", b =>
                {
                    b.Property<int>("ModificadorVariavelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DadoSimplesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("ModificadorVariavelId");

                    b.HasIndex("DadoSimplesId");

                    b.ToTable("ModificadoresVariaveis");
                });

            modelBuilder.Entity("Dicenamics.Models.Sala", b =>
                {
                    b.Property<int>("SalaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdSimples")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioMestreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SalaId");

                    b.HasIndex("UsuarioMestreId");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("Dicenamics.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nickname")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SalaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.HasIndex("SalaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Dicenamics.Models.DadoCompostoSala", b =>
                {
                    b.HasBaseType("Dicenamics.Models.DadoComposto");

                    b.Property<bool>("AcessoPrivado")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CriadorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DadoCompostoSalaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SalaId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("CriadorId");

                    b.HasIndex("SalaId");

                    b.ToTable("DadosCompostosSalas", (string)null);
                });

            modelBuilder.Entity("Dicenamics.Models.DadoSimplesSala", b =>
                {
                    b.HasBaseType("Dicenamics.Models.DadoSimples");

                    b.Property<bool>("AcessoPrivado")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CriadorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DadoSimplesSalaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SalaId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("CriadorId");

                    b.HasIndex("SalaId");

                    b.ToTable("DadosSimplesSalas", (string)null);
                });

            modelBuilder.Entity("Dicenamics.Models.DadoComposto", b =>
                {
                    b.HasOne("Dicenamics.Models.Usuario", null)
                        .WithMany("DadosCompostosPessoais")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Dicenamics.Models.DadoSimples", b =>
                {
                    b.HasOne("Dicenamics.Models.Usuario", null)
                        .WithMany("DadosSimplesPessoais")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Dicenamics.Models.ModificadorVariavel", b =>
                {
                    b.HasOne("Dicenamics.Models.DadoSimples", "Dado")
                        .WithMany()
                        .HasForeignKey("DadoSimplesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dado");
                });

            modelBuilder.Entity("Dicenamics.Models.Sala", b =>
                {
                    b.HasOne("Dicenamics.Models.Usuario", "UsuarioMestre")
                        .WithMany()
                        .HasForeignKey("UsuarioMestreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioMestre");
                });

            modelBuilder.Entity("Dicenamics.Models.Usuario", b =>
                {
                    b.HasOne("Dicenamics.Models.Sala", null)
                        .WithMany("Convidados")
                        .HasForeignKey("SalaId");
                });

            modelBuilder.Entity("Dicenamics.Models.DadoCompostoSala", b =>
                {
                    b.HasOne("Dicenamics.Models.Usuario", "Criador")
                        .WithMany()
                        .HasForeignKey("CriadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dicenamics.Models.DadoComposto", null)
                        .WithOne()
                        .HasForeignKey("Dicenamics.Models.DadoCompostoSala", "DadoCompostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dicenamics.Models.Sala", null)
                        .WithMany("DadosCompostosSala")
                        .HasForeignKey("SalaId");

                    b.Navigation("Criador");
                });

            modelBuilder.Entity("Dicenamics.Models.DadoSimplesSala", b =>
                {
                    b.HasOne("Dicenamics.Models.Usuario", "Criador")
                        .WithMany()
                        .HasForeignKey("CriadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dicenamics.Models.DadoSimples", null)
                        .WithOne()
                        .HasForeignKey("Dicenamics.Models.DadoSimplesSala", "DadoSimplesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dicenamics.Models.Sala", null)
                        .WithMany("DadosSimplesSala")
                        .HasForeignKey("SalaId");

                    b.Navigation("Criador");
                });

            modelBuilder.Entity("Dicenamics.Models.Sala", b =>
                {
                    b.Navigation("Convidados");

                    b.Navigation("DadosCompostosSala");

                    b.Navigation("DadosSimplesSala");
                });

            modelBuilder.Entity("Dicenamics.Models.Usuario", b =>
                {
                    b.Navigation("DadosCompostosPessoais");

                    b.Navigation("DadosSimplesPessoais");
                });
#pragma warning restore 612, 618
        }
    }
}
