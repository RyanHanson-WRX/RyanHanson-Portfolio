using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoughnutDreamsBrewedBeans.Models;

public partial class DDBBDbContext : DbContext
{
    public DDBBDbContext()
    {
    }

    public DDBBDbContext(DbContextOptions<DDBBDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeliveryLocation> DeliveryLocations { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Configuration.GetConnectionString(\"DDBBConnection\")");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC078F4EF445");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3214EC0758BBE40C");

            entity.HasOne(d => d.Station).WithMany(p => p.Items).HasConstraintName("Item_Fk_Station");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC075F7695FA");

            entity.HasOne(d => d.Delivery).WithMany(p => p.Orders).HasConstraintName("Order_Fk_Delivery");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("Order_Fk_Store");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07BE075742");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasConstraintName("OrderItem_Fk_Item");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("OrderItem_Fk_Order");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC07EC1E6453");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC070B9D8C19");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
