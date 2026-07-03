using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public DateTime BirthDate { set; get; }
        public string FatherName { set; get; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
       
        public string PhoneNumber { set; get; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^09\d{9}$",
         ErrorMessage = "شماره ملی معتبر نیست")]
        public string NationalCode { set; get; }
        public Gender Gender { set; get; }
        public bool  IsActive { set; get; }
        public string Subject { set; get; }
        [Required]
        [StringLength(150)]
        public string Adress { set; get; }
        [Required]
        public float  ServicHistory   { set; get; }//سابقه خدمت
        public DateTime HireDate    { set; get; }//تاریخ استخدام

    }
}
