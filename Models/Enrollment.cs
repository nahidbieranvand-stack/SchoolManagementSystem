namespace SchoolManagementSystem.Models
{
    public class Enrollment
    {
        public  int Id { get; set; }
        public int CourseId { get; set; }
        
        public int StudentId { get; set; }
        public decimal Score { get; set; }
        public DateTime EnrollDate { get; set; }

    }
}
