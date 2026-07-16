using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.ViewModels
{
    public class StudentListViewModel
    {
        // لیست دانش آموزان
        public List<Student> Students { get; set; } = new();

        // صفحه بندی
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalStudents { get; set; }

        // جستجو
        public string? Search { get; set; }

        // مرتب سازی
        public string? SortOrder { get; set; }

        public string? NameSortParm { get; set; }
       
    }
}
