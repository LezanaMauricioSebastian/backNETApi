using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backNETApi.Models;

public partial class DbemployeeContext : DbContext
{
    public DbemployeeContext()
    {

    }

    public DbemployeeContext(DbContextOptions<DbemployeeContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).HasName("Department_PK_idDepartment");

            entity.ToTable("Department");

            entity.Property(e => e.IdDepartment).HasColumnName("idDepartment");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("Employee_PK_idEmployee");

            entity.ToTable("Employee");

            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.ContractDate)
                .HasColumnType("datetime")
                .HasColumnName("contractDate");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.IdDepartment).HasColumnName("idDepartment");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_FK_idDepartment");
        });

      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
