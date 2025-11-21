using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoListMVC.Data.Entities;

namespace TodoListMVC.Data;

public partial class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SysMenu> SysMenus { get; set; }

    public virtual DbSet<TDCategory> TDCategories { get; set; }

    public virtual DbSet<TDTask> TDTasks { get; set; }

    public virtual DbSet<TDTaskCategory> TDTaskCategories { get; set; }

    public virtual DbSet<UsGroup> UsGroups { get; set; }

    public virtual DbSet<UsGroupPermission> UsGroupPermissions { get; set; }

    public virtual DbSet<UsUser> UsUsers { get; set; }

    public virtual DbSet<UsUserLog> UsUserLogs { get; set; }

    public virtual DbSet<UsUserPermission> UsUserPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SysMenu>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("SysMenu", tb => tb.HasTrigger("tr_SysMenu_AfterInsert_AddGroupPerms"));

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(1);
            entity.Property(e => e.Note)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ParentMenu).HasMaxLength(50);
        });

        modelBuilder.Entity<TDCategory>(entity =>
        {
            entity.ToTable("TDCategory");

            entity.HasIndex(e => new { e.UserId, e.Name }, "UQ_TDCategory_Name_User").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(3)
                .HasDefaultValueSql("(right(replicate('0',(3))+CONVERT([nvarchar](10),NEXT VALUE FOR [dbo].[Seq_TDCategory]),(3)))");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.RowStatus).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);
            entity.Property(e => e.UserId).HasMaxLength(8);

            entity.HasOne(d => d.User).WithMany(p => p.TDCategories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TDCategory_UsUser");
        });

        modelBuilder.Entity<TDTask>(entity =>
        {
            entity.ToTable("TDTask");

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .HasDefaultValueSql("(right(replicate('0',(12))+CONVERT([nvarchar](30),NEXT VALUE FOR [dbo].[Seq_TDTask]),(12)))");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasDefaultValue(3);
            entity.Property(e => e.RowStatus).HasDefaultValue(true);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(8);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TDTaskCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDTask_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TDTaskUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_TDTask_UpdatedBy");

            entity.HasOne(d => d.User).WithMany(p => p.TDTaskUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDTask_UsUser");
        });

        modelBuilder.Entity<TDTaskCategory>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.CategoryId });

            entity.ToTable("TDTaskCategory");

            entity.Property(e => e.TaskId).HasMaxLength(30);
            entity.Property(e => e.CategoryId).HasMaxLength(3);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TDTaskCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDTC_TDCategory");

            entity.HasOne(d => d.Task).WithMany(p => p.TDTaskCategories)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDTC_TDTask");
        });

        modelBuilder.Entity<UsGroup>(entity =>
        {
            entity.ToTable("UsGroup", tb => tb.HasTrigger("tr_UsGroup_AfterInsert_AddMenuPerms"));

            entity.Property(e => e.Id)
                .HasMaxLength(3)
                .HasDefaultValueSql("(right(replicate('0',(3))+CONVERT([nvarchar](10),NEXT VALUE FOR [dbo].[Seq_UsGroup]),(3)))");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(8);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.RowStatus).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);
        });

        modelBuilder.Entity<UsGroupPermission>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.MenuId });

            entity.ToTable("UsGroupPermission");

            entity.Property(e => e.GroupId).HasMaxLength(3);
            entity.Property(e => e.MenuId).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);

            entity.HasOne(d => d.Group).WithMany(p => p.UsGroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsGroupPermission_UsGroup");

            entity.HasOne(d => d.Menu).WithMany(p => p.UsGroupPermissions)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsGroupPermission_SysMenu");
        });

        modelBuilder.Entity<UsUser>(entity =>
        {
            entity.ToTable("UsUser");

            entity.HasIndex(e => e.UserName, "UQ_UsUser_UserName").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(8)
                .HasDefaultValueSql("(right(replicate('0',(8))+CONVERT([nvarchar](20),NEXT VALUE FOR [dbo].[Seq_UsUser]),(8)))");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CMND).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GroupId).HasMaxLength(3);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RowStatus).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Group).WithMany(p => p.UsUsers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_UsUser_usGroup");
        });

        modelBuilder.Entity<UsUserLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_usUserLog");

            entity.ToTable("UsUserLog");

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .HasDefaultValueSql("(right(replicate('0',(30))+CONVERT([nvarchar](30),NEXT VALUE FOR [dbo].[Seq_UsUserLog]),(30)))");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ComputerName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Data).HasMaxLength(3000);
            entity.Property(e => e.Menu)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(2000);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(8);

            entity.HasOne(d => d.User).WithMany(p => p.UsUserLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usUserLog_UsUser");
        });

        modelBuilder.Entity<UsUserPermission>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MenuId }).HasName("PK_usUserPermission");

            entity.ToTable("UsUserPermission");

            entity.Property(e => e.UserId).HasMaxLength(8);
            entity.Property(e => e.MenuId).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(8);

            entity.HasOne(d => d.Menu).WithMany(p => p.UsUserPermissions)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usUserPermission_SysMenu");

            entity.HasOne(d => d.User).WithMany(p => p.UsUserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usUserPermission_UsUser");
        });
        modelBuilder.HasSequence("Seq_TDCategory").HasMin(1L);
        modelBuilder.HasSequence("Seq_TDTask").HasMin(1L);
        modelBuilder.HasSequence<int>("Seq_UsGroup");
        modelBuilder.HasSequence<int>("Seq_UsUser").StartsAt(100001L);
        modelBuilder.HasSequence("Seq_UsUserLog");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
