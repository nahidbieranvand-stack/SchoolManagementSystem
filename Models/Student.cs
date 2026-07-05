using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Student
    {
        public int Id { set; get; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام الزامی است.")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = " نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است.")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = " کدملی")]
        [Required(ErrorMessage = "کد ملی الزامی است.")]
        [StringLength(10,
          MinimumLength = 10,
          ErrorMessage = "کد ملی باید 10 رقم باشد.")]
        public string NationalCode{ set; get; }
        [Display(Name = " تاریخ تولد")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { set; get; }
        [Display(Name = " اسم پدر")]
        [Required(ErrorMessage = "نام پدر الزامی است.")]
        public string FatherName{ set; get; }
        [Display(Name = "   شماره تلفن")]
        [Required(ErrorMessage = "شماره تلفن الزامی است.")]
        
        [StringLength (11,MinimumLength =11)]
        [RegularExpression(@"^09\d{9}$",
         ErrorMessage = "شماره همراه معتبر نیست")]
        public string PhoneNumber { set; get; }
        [Display(Name = " فعال بودن ")]
        public bool IsActive { set; get; }
        [Display(Name = " پایه تحصیلی")]
        [Range(1,12)]
        public  int Grade {  set; get; }
        
        public DateTime RegisterDate { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "جنسیت الزامی است.")]
        public Gender Gender { set; get; }
    }
}
