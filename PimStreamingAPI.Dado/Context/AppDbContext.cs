using Microsoft.EntityFrameworkCore;
using PimStreamingAPI.Dominio.Entidades;

namespace PimStreamingAPI.Dado.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de Usuário
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração de Playlist
            modelBuilder.Entity<Playlist>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Conteudos)
                .WithOne(c => c.Playlist)
                .HasForeignKey(c => c.PlaylistID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração de Conteúdo
            modelBuilder.Entity<Conteudo>()
                .HasKey(c => c.ID);

            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Criador)
                .WithMany()
                .HasForeignKey(c => c.CriadorID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
