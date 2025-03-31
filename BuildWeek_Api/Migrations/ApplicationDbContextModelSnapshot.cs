﻿// <auto-generated />
using System;
using BuildWeek_Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildWeek_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BuildWeek_Api.Models.Animale", b =>
                {
                    b.Property<Guid>("AnimaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodiceFiscaleProprietario")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CognomeProprietario")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ColoreMantello")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("DataNascita")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataRegistrazione")
                        .HasColumnType("datetime2");

                    b.Property<bool>("MicrochipPresente")
                        .HasColumnType("bit");

                    b.Property<string>("NomeAnimale")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NomeProprietario")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroMicrochip")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tipologia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AnimaleId");

                    b.HasIndex("CodiceFiscaleProprietario");

                    b.HasIndex("NumeroMicrochip")
                        .IsUnique()
                        .HasFilter("[NumeroMicrochip] IS NOT NULL");

                    b.ToTable("Animali");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Veterinario",
                            NormalizedName = "VETERINARIO"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Infermiere",
                            NormalizedName = "INFERMIERE"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Utente",
                            NormalizedName = "UTENTE"
                        });
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Cliente", b =>
                {
                    b.Property<string>("CodiceFiscale")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CodiceFiscale");

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Posizione", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodiceArmadietto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroCassetto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Posizione");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Prodotto", b =>
                {
                    b.Property<Guid>("ProdottoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DittaFornitrice")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("PosizioneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdottoUso")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProdottoId");

                    b.HasIndex("PosizioneId")
                        .IsUnique();

                    b.ToTable("Prodotti");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Ricovero", b =>
                {
                    b.Property<Guid>("RicoveroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnimaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ColoreMantello")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataInizio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataNascita")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("MicrochipPresente")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroMicrochip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipologia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RicoveroId");

                    b.HasIndex("AnimaleId");

                    b.ToTable("Ricoveri");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Vendita", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodiceFiscaleCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DataVendita")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroRicetta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProdottoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnimaleId");

                    b.HasIndex("CodiceFiscaleCliente");

                    b.HasIndex("ProdottoId");

                    b.ToTable("Vendite");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Visita", b =>
                {
                    b.Property<Guid>("VisitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CuraPrescritta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataVisita")
                        .HasColumnType("datetime2");

                    b.Property<string>("EsameObiettivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VisitaId");

                    b.HasIndex("AnimaleId");

                    b.ToTable("Visite");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Animale", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Cliente", "Proprietario")
                        .WithMany("Animali")
                        .HasForeignKey("CodiceFiscaleProprietario")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Proprietario");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationUserRole", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationRole", "ApplicationRole")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationUser", "ApplicationUser")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationRole");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Prodotto", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Posizione", "Posizione")
                        .WithOne("Prodotto")
                        .HasForeignKey("BuildWeek_Api.Models.Prodotto", "PosizioneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Posizione");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Ricovero", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Animale", "Animale")
                        .WithMany("Ricoveri")
                        .HasForeignKey("AnimaleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Animale");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Vendita", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Animale", "Animale")
                        .WithMany("Vendite")
                        .HasForeignKey("AnimaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildWeek_Api.Models.Cliente", "Cliente")
                        .WithMany("Vendite")
                        .HasForeignKey("CodiceFiscaleCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BuildWeek_Api.Models.Prodotto", "Prodotto")
                        .WithMany("Vendite")
                        .HasForeignKey("ProdottoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animale");

                    b.Navigation("Cliente");

                    b.Navigation("Prodotto");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Visita", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Animale", "Animale")
                        .WithMany("Visite")
                        .HasForeignKey("AnimaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animale");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BuildWeek_Api.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Animale", b =>
                {
                    b.Navigation("Ricoveri");

                    b.Navigation("Vendite");

                    b.Navigation("Visite");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Auth.ApplicationUser", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Cliente", b =>
                {
                    b.Navigation("Animali");

                    b.Navigation("Vendite");
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Posizione", b =>
                {
                    b.Navigation("Prodotto")
                        .IsRequired();
                });

            modelBuilder.Entity("BuildWeek_Api.Models.Prodotto", b =>
                {
                    b.Navigation("Vendite");
                });
#pragma warning restore 612, 618
        }
    }
}
