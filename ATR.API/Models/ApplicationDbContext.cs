using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ATR.API.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Carusel> Carusels { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<PageStatistic> PageStatistics { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomProperty> RoomProperties { get; set; }

    public virtual DbSet<TeamWork> TeamWorks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=223-10;Database=Customers; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Carusel>(entity =>
        {
            entity.ToTable("Carusel");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(2500);
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.PictureUrl).HasMaxLength(500);
            entity.Property(e => e.Tirle).HasMaxLength(500);
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.ToTable("JobPosition");

            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.Ip)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.Properties).HasColumnType("xml");
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<PageStatistic>(entity =>
        {
            entity.HasKey(e => e.PageStatisticsId);

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Path).HasMaxLength(500);
            entity.Property(e => e.PathBase).HasMaxLength(500);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.MainPicturePath).HasDefaultValueSql("(N'')");

            //entity.HasOne(d => d.Category).WithMany(p => p.Rooms).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<RoomProperty>(entity =>
        {
            entity.HasMany(d => d.RoomsRooms).WithMany(p => p.RoomPropertiesRoomProperties)
                .UsingEntity<Dictionary<string, object>>(
                    "RoomRoomProperty",
                    r => r.HasOne<Room>().WithMany().HasForeignKey("RoomsRoomId"),
                    l => l.HasOne<RoomProperty>().WithMany().HasForeignKey("RoomPropertiesRoomPropertyId"),
                    j =>
                    {
                        j.HasKey("RoomPropertiesRoomPropertyId", "RoomsRoomId");
                        j.ToTable("RoomRoomProperty");
                    });
        });

        modelBuilder.Entity<TeamWork>(entity =>
        {
            entity.ToTable("TeamWork");

            entity.Property(e => e.AboutWork).HasMaxLength(1024);
            entity.Property(e => e.CreateUser).HasMaxLength(250);
            entity.Property(e => e.FaceBook).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.Instagram).HasMaxLength(250);
            entity.Property(e => e.JobPositionId).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.LinkedIn).HasMaxLength(250);
            entity.Property(e => e.MiddleName).HasMaxLength(250);
            entity.Property(e => e.Photo).HasMaxLength(1024);
            entity.Property(e => e.Status).HasMaxLength(250);

            entity.HasOne(d => d.JobPositionName).WithMany(p => p.TeamWorks).HasForeignKey(d => d.JobPositionNameId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
