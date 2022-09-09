using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication3.Model
{
    public partial class FirstExamContext : DbContext
    {
        public bool IgnoreFilter { get; set; }
        public FirstExamContext()
        {
        }

        public FirstExamContext(DbContextOptions<FirstExamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<CsvView> CsvViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\;Database=FirstExam;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Archived).HasColumnName("archived");

                entity.Property(e => e.CraetedAt)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryId");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.SubcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__item__subcategor__4222D4EF");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.ToTable("subcategory");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("date");

                entity.Property(e => e.Name)    
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Subcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__subcatego__categ__3D5E1FD2");
            });
            modelBuilder.Entity<CsvView>(
                entity =>
                {
                    entity.ToView("cvsview");
                    entity.Property(e => e.ItemId).HasColumnName("ItemId");
                    entity.Property(e => e.SubcategoryId).HasColumnName("SubcategoryId");
                    entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                    entity.Property(e => e.ItemName).HasColumnName("ItemName");
                    entity.Property(e => e.SubName).HasColumnName("SubName");
                    entity.Property(e => e.CategoryName).HasColumnName("CategoryName");
                    entity.Property(e => e.Archived).HasColumnName("Archived");
                    entity.HasNoKey();
                }
                );
            modelBuilder.Entity<Item>().HasQueryFilter(x => !x.Archived || IgnoreFilter);
            modelBuilder.Entity<Subcategory>().HasQueryFilter(x => !x.Archived || IgnoreFilter);
            modelBuilder.Entity<Category>().HasQueryFilter( x=> !x.Archived || IgnoreFilter);
            modelBuilder.Entity<CsvView>().HasQueryFilter( x=> !x.Archived || IgnoreFilter);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
