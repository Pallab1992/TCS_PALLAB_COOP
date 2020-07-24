using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace LHV.Model
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> Student { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<DepartmentCourse> DepartmentCourse {get; set;}
        public DbSet<StudentCourse> StudentCourse {get; set;}

        public StudentDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasIndex(s => s.StudentRoll)
                .IsUnique();

            builder.Entity<Student>()
                .HasIndex(s => s.StudentRegistrationNo)
                .IsUnique();

            builder.Entity<Department>()
                .HasIndex(d => d.DepartmentCode)
                .IsUnique();

            builder.Entity<Course>()
                .HasIndex(c => c.CourseCode)
                .IsUnique();

            builder.Entity<Student>()
                .HasIndex(s => s.StudentRegistrationNo)
                .IsUnique();

            builder.Entity<Student>()
                .Property(s => s.StudentRegistrationNo)
                .HasMaxLength(15);

            builder.Entity<Student>()
                .Property(s => s.StudentName)
                .HasMaxLength(30);

            builder.Entity<Student>()
                .Property(s => s.StudentRoll)
                .HasMaxLength(10);

            builder.Entity<Student>()
                .Property(s => s.StudentAddress)
                .HasMaxLength(30);

            builder.Entity<Student>()
                .Property(s => s.StudentContactNo)
                .HasMaxLength(10);

            builder.Entity<Department>()
                .Property(d => d.DepartmentName)
                .HasMaxLength(30);

            builder.Entity<Department>()
                .Property(d => d.DepartmentCode)
                .HasMaxLength(3);

            builder.Entity<Course>()
                .Property(c => c.CourseName)
                .HasMaxLength(30);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            }
        }
    }
}