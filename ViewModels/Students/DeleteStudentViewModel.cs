using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels.Students
{
    public class DeleteStudentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }

       
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        
        [Display(Name = "تاریخ تولد")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "نام پدر")]
        public string? FatherName { get; set; }

        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; }
        public List<SelectListItem> GenderList { get; set; } = new();

        [Display(Name = "پایه تحصیلی")]
        public int Grade
        {
            get; set;
        }
        public List<SelectListItem> GradeList { get; set; } = new();
    }
}

