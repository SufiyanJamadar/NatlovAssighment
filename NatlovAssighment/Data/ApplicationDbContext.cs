using Microsoft.EntityFrameworkCore;
using NatlovAssighment.Models;

namespace NatlovAssighment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CourseSchedule> CourseSchedules { get; set; }

        public DbSet<StudentCourse> StudentCourses{get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseSchedule>()
                .HasOne(cs => cs.Course)
                .WithMany()
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Keeps cascade delete for Course

            modelBuilder.Entity<CourseSchedule>()
                .HasOne(cs => cs.Teacher)
                .WithMany()
                .HasForeignKey(cs => cs.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents multiple cascade paths issue

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany()
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Course

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany()
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Student

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Teacher)
                .WithMany()
                .HasForeignKey(sc => sc.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents multiple cascade paths issue

            base.OnModelCreating(modelBuilder);
        }


    }



}