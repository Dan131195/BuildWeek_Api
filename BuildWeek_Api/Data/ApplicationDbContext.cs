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

        public DbSet<ApplicationUser> ApplicationUsers {get; set;}
        public DbSet<ApplicationRole> ApplicationRoles {get; set;}
        public DbSet<ApplicationUserRole> ApplicationUserRoles {get; set;}

        public DbSet<Animale> Animali {get; set;}
        public DbSet<Ricovero> Ricoveri {get; set;}
        public DbSet<Visita> Visite {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Animale>()
                .HasIndex(a => a.NumeroMicrochip)
                .IsUnique()
                .HasFilter("[NumeroMicrochip] IS NOT NULL");

            builder.Entity<Ricovero>()
                .HasOne(r => r.Animale)
                .WithMany(a => a.Ricoveri)
                .HasForeignKey(r => r.AnimaleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
