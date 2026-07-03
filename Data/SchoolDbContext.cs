namespace SchoolManagementSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using SchoolManagementSystem.Models;

    public class SchoolDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }
    }
    
}
