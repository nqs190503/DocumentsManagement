﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentsManagementSystem.Models
{
    public partial class _13_NguyenQuangSu_TranLongKhanh_ProjectContext : DbContext
    {
        public _13_NguyenQuangSu_TranLongKhanh_ProjectContext()
        {
        }

        public _13_NguyenQuangSu_TranLongKhanh_ProjectContext(DbContextOptions<_13_NguyenQuangSu_TranLongKhanh_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Userdetail> Userdetails { get; set; } = null!;
        public virtual DbSet<Version> Versions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-GVNBRE21;uid=sa;pwd=123456789;database=\n13_NguyenQuangSu_TranLongKhanh_Project;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.ToTable("documents");

                entity.Property(e => e.FileId).HasColumnName("fileId");

                entity.Property(e => e.FileContent)
                    .IsUnicode(false)
                    .HasColumnName("fileContent");

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fileName");

                entity.Property(e => e.FileStatus).HasColumnName("fileStatus");

                entity.Property(e => e.LastModifier)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModifier");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_documents_userdetails");
            });

            modelBuilder.Entity<Userdetail>(entity =>
            {
                entity.ToTable("userdetails");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.ToTable("versions");

                entity.Property(e => e.VersionId).HasColumnName("versionID");

                entity.Property(e => e.DocId).HasColumnName("docID");

                entity.Property(e => e.UpdatedContent).HasColumnName("updatedContent");

                entity.Property(e => e.UpdatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedTime");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Versions)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_versions_documents");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
