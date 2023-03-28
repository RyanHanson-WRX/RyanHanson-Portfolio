using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DbProject.Web.Models;

public partial class SubscriptionServicesContext : IdentityDbContext<IdentityUser>
{
    public SubscriptionServicesContext()
    {
    }

    public SubscriptionServicesContext(DbContextOptions<SubscriptionServicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyStat> CompanyStats { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubStat> UserSubStats { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<UserTotalStat> UserTotalStats { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SubscriptionServices");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC27F0C54D3F");

            entity.HasIndex(e => e.Name, "UQ__Companie__737584F6EAB02F47").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<CompanyStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyS__3214EC27E3964705");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.YearlyEarnings).HasColumnType("money");

            /*entity.HasOne(d => d.Company).WithMany(p => p.CompanyStats)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompanySt__Compa__4BAC3F29");*/
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC274BBEDE13");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.MonthlyPrice).HasColumnType("money");
            entity.Property(e => e.SubscriptionTier)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('Standard')");

           /* entity.HasOne(d => d.Company).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Compa__3F466844");*/
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC279330C401");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053408A56E2F").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456DA193BBC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspNetUserId)
                .HasMaxLength(128)
                .HasColumnName("AspNetUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserSubStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubS__3214EC2771B1EBFC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RunningTotal).HasColumnType("money");
            entity.Property(e => e.UserSubId).HasColumnName("UserSubID");
            entity.Property(e => e.YearlyCost).HasColumnType("money");

            entity.HasOne(d => d.UserSub).WithMany(p => p.UserSubStats)
                .HasForeignKey(d => d.UserSubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubSt__UserS__48CFD27E");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubs__3214EC27ED82C2FC");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("CompanyStats_INSERT");
                    tb.HasTrigger("CompanyStats_UPDATE");
                    tb.HasTrigger("UserSubStats_INSERT");
                    tb.HasTrigger("UserSubStats_UPDATE");
                    tb.HasTrigger("UserSubscriptions_DELETE");
                    tb.HasTrigger("UserTotalStats_INSERT");
                    tb.HasTrigger("UserTotalStats_UPDATE");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            /*entity.HasOne(d => d.Subscription).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Subsc__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__UserI__4316F928");*/
        });

        modelBuilder.Entity<UserTotalStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTota__3214EC27CA8FEB62");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AllSubRunningTotal).HasColumnType("money");
            entity.Property(e => e.TotalSubYearlyCost).HasColumnType("money");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            /*entity.HasOne(d => d.User).WithMany(p => p.UserTotalStats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserTotal__UserI__45F365D3");*/
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
