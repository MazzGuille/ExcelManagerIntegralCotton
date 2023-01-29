using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExcelManagerIntegralCotton.Models;

public partial class IntegralCottonDbContext : DbContext
{
    public IntegralCottonDbContext()
    {
    }

    public IntegralCottonDbContext(DbContextOptions<IntegralCottonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExcelDatum> ExcelData { get; set; }

    public virtual DbSet<FileUpload> FileUploads { get; set; }

    public virtual DbSet<Hvi> Hvis { get; set; }

    public virtual DbSet<HviTitle> HviTitles { get; set; }

    public virtual DbSet<User> Users { get; set; }  
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExcelDatum>(entity =>
        {
            entity.Property(e => e.Colorgrade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MIC");
            entity.Property(e => e.Sfi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SFI");
            entity.Property(e => e.Strength)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrashId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Uhml)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UHML");
            entity.Property(e => e.Ui)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UI");
        });

        modelBuilder.Entity<FileUpload>(entity =>
        {
            entity.ToTable("FileUpload");

            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Hvi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Upload");

            entity.ToTable("HVI");

            entity.Property(e => e.Colorgrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COLORGRADE");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Mic)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MIC");
            entity.Property(e => e.Sfi)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SFI");
            entity.Property(e => e.Strength)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("STRENGTH");
            entity.Property(e => e.Trashid)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TRASHID");
            entity.Property(e => e.Uhml)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UHML");
            entity.Property(e => e.Ui)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UI");

            entity.HasOne(d => d.FileNameNavigation).WithMany(p => p.Hvis)
                .HasForeignKey(d => d.FileName)
                .HasConstraintName("FK_HVI_HviTitles");
        });

        modelBuilder.Entity<HviTitle>(entity =>
        {
            entity.HasKey(e => e.TitleId);

            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
