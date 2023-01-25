﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livros
{
    public class AppDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivroAutor> LivrosAutores { get; set; }
        private readonly string _connectionString;
        public AppDbContext()
        {
            //_connectionString = @"Data Source=DESKTOP-A59E2IP\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=True";
            _connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=aspnet-Livro;Integrated Security=SSPI"; ;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LivroAutor>()
                    .HasKey(x => new { x.LivroId, x.AutorId });
            //definindo o relacionamento explicitamente
            modelBuilder.Entity<LivroAutor>()
            .HasOne(bc => bc.Livro)
             .WithMany(b => b.LivrosAutores)
              .HasForeignKey(bc => bc.LivroId);
            modelBuilder.Entity<LivroAutor>()
                .HasOne(bc => bc.Autor)
                .WithMany(c => c.LivrosAutores)
                 .HasForeignKey(bc => bc.AutorId);
        }
    }
}
