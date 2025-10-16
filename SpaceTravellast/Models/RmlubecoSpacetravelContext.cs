using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SpaceTravellast.Models;

public partial class RmlubecoSpacetravelContext : DbContext
{
    public RmlubecoSpacetravelContext()
    {
    }

    public RmlubecoSpacetravelContext(DbContextOptions<RmlubecoSpacetravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<Turlar> Turlars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=p1420.use1.mysecurecloudhost.com;Database=rmlubeco_spacetravel;User Id=rmlubeco_spacetravel;Password=osBx7!_Y73nolfQh;Encrypt=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("rmlubeco_spacetravel");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BD526043A");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21B76B7F2D78B");

            entity.ToTable("City");

            entity.Property(e => e.CityName).HasMaxLength(60);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCAA07B6E94");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentEmail).HasMaxLength(100);
            entity.Property(e => e.CommentTittle).HasMaxLength(60);
            entity.Property(e => e.CommentUsername).HasMaxLength(60);

            entity.HasOne(d => d.CommentHotel).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentHotelId)
                .HasConstraintName("FK__Comment__Comment__7C4F7684");

            entity.HasOne(d => d.CommentNews).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentNewsId)
                .HasConstraintName("FK__Comment__Comment__7E37BEF6");

            entity.HasOne(d => d.CommentTurlar).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentTurlarId)
                .HasConstraintName("FK__Comment__Comment__7D439ABD");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact__5C66259B3CC929BB");

            entity.ToTable("Contact");

            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D1609F42D3CCC7");

            entity.ToTable("Country");

            entity.Property(e => e.CountryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PK__Currency__14470AF0F693E3F7");

            entity.ToTable("Currency");

            entity.Property(e => e.CurrencyName).HasMaxLength(50);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotel__46023BDF21872700");

            entity.ToTable("Hotel");

            entity.Property(e => e.HotelName).HasMaxLength(50);

            entity.HasOne(d => d.HotelCity).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.HotelCityId)
                .HasConstraintName("FK__Hotel__HotelCity__72C60C4A");

            entity.HasOne(d => d.HotelCurrency).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.HotelCurrencyId)
                .HasConstraintName("FK__Hotel__HotelCurr__73BA3083");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDF3E4A64E25");

            entity.Property(e => e.NewsName).HasMaxLength(50);
            entity.Property(e => e.NewsTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photo__21B7B5E200F4F353");

            entity.ToTable("Photo");

            entity.Property(e => e.PhotoName).HasMaxLength(50);

            entity.HasOne(d => d.PhotoHotel).WithMany(p => p.Photos)
                .HasForeignKey(d => d.PhotoHotelId)
                .HasConstraintName("FK__Photo__PhotoHote__02084FDA");

            entity.HasOne(d => d.PhotoTurlar).WithMany(p => p.Photos)
                .HasForeignKey(d => d.PhotoTurlarId)
                .HasConstraintName("FK__Photo__PhotoTurl__01142BA1");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00AB7DCEE89");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceName).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2043054E42C2");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.SubscriberId).HasName("PK__Subscrib__7DFEB6D47E9C84A2");

            entity.ToTable("Subscriber");

            entity.Property(e => e.SubscriberEmail).HasMaxLength(100);
            entity.Property(e => e.SubscriberTitle).HasMaxLength(60);
            entity.Property(e => e.SubscriberUsername).HasMaxLength(60);
        });

        modelBuilder.Entity<Turlar>(entity =>
        {
            entity.HasKey(e => e.TurlarId).HasName("PK__Turlar__5854F1D6BF97620D");

            entity.ToTable("Turlar");

            entity.Property(e => e.TurAd).HasMaxLength(50);
            entity.Property(e => e.TurPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TurTarix).HasColumnType("datetime");

            entity.HasOne(d => d.TurlarCategory).WithMany(p => p.Turlars)
                .HasForeignKey(d => d.TurlarCategoryId)
                .HasConstraintName("FK__Turlar__TurlarCa__76969D2E");

            entity.HasOne(d => d.TurlarCountry).WithMany(p => p.Turlars)
                .HasForeignKey(d => d.TurlarCountryId)
                .HasConstraintName("FK__Turlar__TurlarCo__787EE5A0");

            entity.HasOne(d => d.TurlarCurrency).WithMany(p => p.Turlars)
                .HasForeignKey(d => d.TurlarCurrencyId)
                .HasConstraintName("FK__Turlar__TurlarCu__778AC167");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C5A5B5AD5");

            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(60);
            entity.Property(e => e.UserPassword).HasMaxLength(50);

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStatusId)
                .HasConstraintName("FK__Users__UserStatu__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
