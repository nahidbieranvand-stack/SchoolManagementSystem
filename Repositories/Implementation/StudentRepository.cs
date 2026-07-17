using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories.Interfaces;
namespace SchoolManagementSystem.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetAll()
        {
            return _context.Students.AsQueryable();
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }
        public  bool NationalCodeExists(string nationalCode)
        {
            return _context.Students
                           .Any(s => s.NationalCode == nationalCode);
        }
    }
}
