using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace HW5_6.Models
{
    public partial class HW3Context : DbContext
    {
        private readonly string connectionString;

        public HW3Context(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public virtual DbSet<Analysis> Analyses { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.HasKey(e => e.AnId)
                    .HasName("PK__Analysis__831DABF3FF4A365B");

                entity.ToTable("Analysis");

                entity.Property(e => e.AnId).HasColumnName("an_id");

                entity.Property(e => e.AnCost)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("an_cost");

                entity.Property(e => e.AnGroup).HasColumnName("an_group");

                entity.Property(e => e.AnName)
                    .HasMaxLength(50)
                    .HasColumnName("an_name");

                entity.Property(e => e.AnPrice)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("an_price");

                entity.HasOne(d => d.AnGroupNavigation)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.AnGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Analysis__an_gro__398D8EEE");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GrId)
                    .HasName("PK__Groups__2BC0F88E9D6646B8");

                entity.Property(e => e.GrId).HasColumnName("gr_id");

                entity.Property(e => e.GrName)
                    .HasMaxLength(20)
                    .HasColumnName("gr_name");

                entity.Property(e => e.GrTemp)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("gr_temp");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrdId)
                    .HasName("PK__Orders__DC39D7DF65DCC694");

                entity.Property(e => e.OrdId).HasColumnName("ord_id");

                entity.Property(e => e.OrdAn).HasColumnName("ord_an");

                entity.Property(e => e.OrdDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("ord_datetime");

                entity.HasOne(d => d.OrdAnNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrdAn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__ord_an__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
