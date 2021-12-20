using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace webApiSample01.Models
{
    public partial class LeaveSystemContext : DbContext
    {

        public LeaveSystemContext(DbContextOptions<LeaveSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApproverList> ApproverList { get; set; }
        public virtual DbSet<ApproverSetting> ApproverSetting { get; set; }
        public virtual DbSet<LeaveStatus> LeaveStatus { get; set; }
        public virtual DbSet<LeaveSubmission> LeaveSubmission { get; set; }
        public virtual DbSet<LeaveType> LeaveType { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApproverList>(entity =>
            {
                entity.HasKey(e => e.ApproverId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.ApproverList)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApproverList_ApproverSetting");
            });

            modelBuilder.Entity<ApproverSetting>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.SettingDescription)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.ApproverSetting)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApproverSetting_UserRoles");
            });

            modelBuilder.Entity<LeaveStatus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveStatus1)
                    .IsRequired()
                    .HasColumnName("LeaveStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LeaveSubmission>(entity =>
            {
                entity.Property(e => e.LeaveSubmissionId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.LeaveSubmissionNavigation)
                    .WithOne(p => p.LeaveSubmission)
                    .HasForeignKey<LeaveSubmission>(d => d.LeaveSubmissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveSubmission_LeaveStatus");

                entity.HasOne(d => d.LeaveSubmission1)
                    .WithOne(p => p.LeaveSubmission)
                    .HasForeignKey<LeaveSubmission>(d => d.LeaveSubmissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveSubmission_LeaveType");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveAmount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LeaveType1)
                    .IsRequired()
                    .HasColumnName("LeaveType")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.LeaveType)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveType_UserRoles");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_useraccount");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserFullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_useraccount_userroles");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_userroles");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
