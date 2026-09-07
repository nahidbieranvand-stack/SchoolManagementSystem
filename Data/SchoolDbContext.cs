
namespace SchoolManagementSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SchoolManagementSystem.Models;
  //  public class SchoolDbContext:DbContext بخاطر استفاده  از خاصیت ایدینتیتی یوزر خود ای اسپی دات نت این به  پایینی تغییر دادیم
         public class SchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }
    }
    
}
