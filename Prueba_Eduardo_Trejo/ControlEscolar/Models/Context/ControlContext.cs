using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ControlEscolar.Models.Entidades;

namespace ControlEscolar.Models.Context
{
    public partial class ControlContext : DbContext
    {
        public ControlContext()
        {
        }

        public ControlContext(DbContextOptions<ControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; } = null!;
        public virtual DbSet<Materia> Materia { get; set; } = null!;
        public virtual DbSet<MateriasAlumno> MateriasAlumno { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Creditos).HasColumnName("creditos");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<MateriasAlumno>(entity =>
            {
                entity.ToTable("Materias_Alumno");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdMateria).HasColumnName("idMateria");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.MateriasAlumno)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MateriasAlumno_Alumno");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.MateriasAlumno)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MateriasAlumno_Materia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
