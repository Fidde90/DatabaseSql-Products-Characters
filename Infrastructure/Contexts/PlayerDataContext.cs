using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class PlayerDataContext : DbContext
{
    public PlayerDataContext()
    {
    }

    public PlayerDataContext(DbContextOptions<PlayerDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KindEntity> Kinds { get; set; }

    public virtual DbSet<PlayerEntity> Players { get; set; }

    public virtual DbSet<PlayerWeaponEntity> PlayerWeapons { get; set; }

    public virtual DbSet<RaceEntity> Races { get; set; }

    public virtual DbSet<WeaponEntity> Weapons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KindEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kinds__3214EC07F0FB7D08");

            entity.HasIndex(e => e.KindName, "UQ__Kinds__4EBDEE08E774498F").IsUnique();

            entity.Property(e => e.KindName).HasMaxLength(100);
        });

        modelBuilder.Entity<PlayerEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Players__3214EC0758F786CC");

            entity.HasIndex(e => e.CharacterName, "UQ__Players__51594B291C03709B").IsUnique();

            entity.Property(e => e.CharacterName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.KindNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.Kind)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__Kind__412EB0B6");

            entity.HasOne(d => d.RaceNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.Race)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__Race__4222D4EF");
        });

        modelBuilder.Entity<PlayerWeaponEntity>(entity =>
        {
            entity.HasKey(e => new { e.Weapon, e.Player }).HasName("PK__PlayerWe__08EB96BFA8BB0A6F");

            entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.PlayerWeapons)
                .HasForeignKey(d => d.Player)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerWea__Playe__44FF419A");

            entity.HasOne(d => d.WeaponNavigation).WithMany(p => p.PlayerWeapons)
                .HasForeignKey(d => d.Weapon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerWea__Weapo__45F365D3");
        });

        modelBuilder.Entity<RaceEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Races__3214EC079CA1D905");

            entity.HasIndex(e => e.RaceName, "UQ__Races__0FC8791EF3D1EA0C").IsUnique();

            entity.Property(e => e.RaceName).HasMaxLength(100);
        });

        modelBuilder.Entity<WeaponEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Weapons__3214EC07BDBBE445");

            entity.HasIndex(e => e.WeaponName, "UQ__Weapons__4EFD147D1AA9A59F").IsUnique();

            entity.Property(e => e.WeaponName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
