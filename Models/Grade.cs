using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
     public enum EducationLevel
    {
        Primary = 1,        // ابتدایی
        MiddleSchool = 2,   // متوسطه اول
        HighSchool = 3      // متوسطه دوم
    }
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // ترتیب نمایش
        public int Order { get; set; }

        // مقطع تحصیلی
        public EducationLevel EducationLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Student> Students { get; set; }
            = new List<Student>();
    }
}
