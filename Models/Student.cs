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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode{ set; get; }
        public string BirthDate { set; get; }
        public string FatherName{ set; get; }
        public string PhoneNumber { set; get; }
        public bool IsActive { set; get; }
        public  int Grade {  set; get; }
        public DateTime RegisterDate { get; set; }
        public Gender Gender { set; get; }
    }
}
