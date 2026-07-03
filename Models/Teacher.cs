using System.Reflection.Metadata;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { set; get; }
        public string FatherName { set; get; }
        public string PhoneNumber { set; get; }
        public string NationalCode { set; get; }
        public Gender Gender { set; get; }
        public bool  IsActive { set; get; }
        public string Subject { set; get; }
        public string Adress { set; get; }
        public float  ServicHistory   { set; get; }//سابقه خدمت
        public DateTime HireDate    { set; get; }//تاریخ استخدام

    }
}
