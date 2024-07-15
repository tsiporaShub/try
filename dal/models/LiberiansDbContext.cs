﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dal.models;

public partial class LiberiansDbContext : DbContext
{
    public LiberiansDbContext()
    {
    }

    public LiberiansDbContext(DbContextOptions<LiberiansDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<BorrowApprovalRequest> BorrowApprovalRequests { get; set; }

    public virtual DbSet<BorrowRequest> BorrowRequests { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemTag> ItemTags { get; set; }

    public virtual DbSet<RatingNote> RatingNotes { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<SearchLog> SearchLogs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=liberiansDB;User Id=sa;Password=Foir100#;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("Activity_Logs");

            entity.HasIndex(e => e.UserId1, "IX_Activity_Logs_UserId1");

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.ActivityLogs).HasForeignKey(d => d.UserId1);
        });

        modelBuilder.Entity<BorrowApprovalRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("Borrow_Approval_Requests");

            entity.HasIndex(e => e.UserId, "IX_Borrow_Approval_Requests_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.BorrowApprovalRequests).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BorrowRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("Borrow_Requests");

            entity.HasIndex(e => e.ItemId, "IX_Borrow_Requests_ItemId");

            entity.HasIndex(e => e.UserId, "IX_Borrow_Requests_UserId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.BorrowRequests).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.User).WithMany(p => p.BorrowRequests).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ItemTag>(entity =>
        {
            entity.ToTable("ItemTag");

            entity.HasIndex(e => e.ItemId, "IX_ItemTag_ItemId");

            entity.HasIndex(e => e.TagId, "IX_ItemTag_TagId");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemTags).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.Tag).WithMany(p => p.ItemTags).HasForeignKey(d => d.TagId);
        });

        modelBuilder.Entity<RatingNote>(entity =>
        {
            entity.ToTable("Rating_Notes");

            entity.HasIndex(e => e.ItemId, "IX_Rating_Notes_ItemId");

            entity.HasIndex(e => e.UserId, "IX_Rating_Notes_UserId");

            entity.HasOne(d => d.Item).WithMany(p => p.RatingNotes).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.User).WithMany(p => p.RatingNotes).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasIndex(e => e.GeneratedByNavigationUserId, "IX_Reports_GeneratedByNavigationUserId");

            entity.HasOne(d => d.GeneratedByNavigationUser).WithMany(p => p.Reports).HasForeignKey(d => d.GeneratedByNavigationUserId);
        });

        modelBuilder.Entity<SearchLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("Search_Logs");

            entity.HasIndex(e => e.UserId, "IX_Search_Logs_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.SearchLogs).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Tz)
                .HasMaxLength(9)
                .HasColumnName("tz");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
