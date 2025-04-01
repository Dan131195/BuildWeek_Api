using System.Reflection.Emit;
using BuildWeek_Api.Models;
using BuildWeek_Api.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Animale> Animali { get; set; }
        public DbSet<Ricovero> Ricoveri { get; set; }
        public DbSet<Visita> Visite { get; set; }
        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Vendita> Vendite { get; set; }
        public DbSet<Posizione> Posizioni { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Animale>()
                .HasIndex(a => a.NumeroMicrochip)
                .IsUnique()
                .HasFilter("[NumeroMicrochip] IS NOT NULL");

            builder.Entity<Animale>()
                .HasMany(a => a.Ricoveri)
                .WithOne(r => r.Animale)
                .HasForeignKey(r => r.AnimaleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Animale>()
                .HasMany(a => a.Vendite)
                .WithOne(v => v.Animale)
                .HasForeignKey(v => v.AnimaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cliente>()
                .HasMany(c => c.Animali)
                .WithOne(a => a.Proprietario)
                .HasForeignKey(a => a.CodiceFiscaleProprietario)
                .HasPrincipalKey(c => c.CodiceFiscale)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cliente>()
                .HasMany(c => c.Vendite)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.CodiceFiscaleCliente)
                .HasPrincipalKey(c => c.CodiceFiscale)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Prodotto>()
                .HasKey(p => p.ProdottoId);

            builder.Entity<Prodotto>()
                .HasOne(p => p.Posizione)
                .WithOne()
                .HasForeignKey<Prodotto>(p => p.PosizioneId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Prodotto>()
                .HasMany(p => p.Vendite)
                .WithOne(v => v.Prodotto)
                .HasForeignKey(v => v.ProdottoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Posizione>()
                .HasKey(pos => pos.Id);


            builder.Entity<ApplicationUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.ApplicationUser)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.ApplicationRole)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "Veterinario", NormalizedName = "VETERINARIO" },
                new ApplicationRole { Id = "2", Name = "Infermiere", NormalizedName = "INFERMIERE" }
            );
        }
    }
}
