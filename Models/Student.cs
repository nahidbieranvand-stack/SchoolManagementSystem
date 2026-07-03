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
        [Required] [StringLength (50)]
        public string FirstName { get; set; }
        [Required] [StringLength (50)]
        public string LastName { get; set; }
        [Required]
        [StringLength (10,MinimumLength =10)]
        [RegularExpression(@"^09\d{9}$",
         ErrorMessage = "شماره ملی معتبر نیست")]
        public string NationalCode{ set; get; }
        [Required]
        public DateTime BirthDate { set; get; }
        [Required]
        public string FatherName{ set; get; }
        [Required]
        [StringLength (11,MinimumLength =11)]
        [RegularExpression(@"^09\d{9}$",
         ErrorMessage = "شماره موبایل معتبر نیست")]
        public string PhoneNumber { set; get; }
        public bool IsActive { set; get; }
        [Range(1,12)]
        public  int Grade {  set; get; }
        public DateTime RegisterDate { get; set; }
        [Required]
        public Gender Gender { set; get; }
    }
}
