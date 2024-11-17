using PimStreamingAPI.Dominio.Entidades;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PimStreamingAPI.Dado.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Carregar o arquivo appsettings.json para obter a string de conexão
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("PimStreamingAPI.Dado"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para auto-incremento nas chaves primárias
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.ID);
            modelBuilder.Entity<Usuario>()
                .Property(u => u.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Playlist>()
                .HasKey(p => p.ID);
            modelBuilder.Entity<Playlist>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Conteudo>()
    .HasOne(c => c.Playlist)
    .WithMany(p => p.Conteudos)
    .HasForeignKey(c => c.PlaylistID)
    .OnDelete(DeleteBehavior.Restrict);



            // Configuração de relacionamentos
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Playlist)
                .WithMany(p => p.Conteudos)
                .HasForeignKey(c => c.PlaylistID)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}