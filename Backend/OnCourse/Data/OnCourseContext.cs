using Microsoft.EntityFrameworkCore;
using OnCourse.Enums;
using OnCourse.Models;

namespace OnCourse.Data
{
    public class OnCourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

        public OnCourseContext(DbContextOptions<OnCourseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(e =>
            {
                e.ToTable("Course");
                e.HasKey(p => p.Id);
                e.Property(p => p.Title).HasColumnType("varchar(128)").IsRequired();
                e.Property(p => p.Duration).HasColumnType("int").IsRequired();
                e.Property(p => p.Status).HasColumnType("tinyint").IsRequired();
            });

            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = 1,
                Title = "C#: Primeiros Passos",
                Duration = 45,
                Status = EnCourseStatus.COMPLETED
            });
            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = 2,
                Title = "C#: Eventos, Delegates e Lambda",
                Duration = 15,
                Status = EnCourseStatus.COMPLETED
            });
            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = 3,
                Title = "Entity Framework Core: Banco de Dados de forma eficiente",
                Duration = 12,
                Status = EnCourseStatus.IN_PROGRESS
            });
            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = 4,
                Title = "Microsserviços na prática: Entendendo a tomada de decisões",
                Duration = 25,
                Status = EnCourseStatus.IN_PROGRESS
            });
            modelBuilder.Entity<Course>().HasData(new Course
            {
                Id = 5,
                Title = "Amazon Code Deploy: Deploy Continuo com AWS",
                Duration = 20,
                Status = EnCourseStatus.SCHEDULED
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(p => p.Id);
                e.Property(p => p.Login).HasColumnType("varchar(32)").IsRequired();
                e.Property(p => p.Password).HasColumnType("varchar(32)").IsRequired();
                e.Property(p => p.Role).HasColumnType("tinyint").IsRequired();
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Login = "dsteixeira",
                Password = "pass1&",
                Role = EnUserRole.MANAGER
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Login = "rwsilva",
                Password = "pass2&",
                Role = EnUserRole.SECRETARY
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                Login = "mcmorais",
                Password = "pass3&",
                Role = EnUserRole.DEFAULT
            });
        }
    }
}
