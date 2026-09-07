using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SchoolManagementSystem.ViewModels.Students
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام الزامی است.")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کد ملی الزامی است.")]
        [RegularExpression(@"^\d{10}$",
          ErrorMessage = "کد ملی باید دقیقاً ۱۰ رقم باشد.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "تاریخ تولد الزامی است.")]
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
        public int GradeId
        {
            get; set;
        }
        public string? GradeName { get; set; }
        public List<SelectListItem> GradeList { get; set; } = new();
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }

       
    }

}

