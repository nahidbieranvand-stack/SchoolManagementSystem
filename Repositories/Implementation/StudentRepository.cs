using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
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
            //return _context.Students.AsQueryable();بخاطر  سافت دیلیت بصورت زیر تغییر میدیم 

            return _context.Students.Include(x => x.Grade)
                .Where(x =>x.IsActive);
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
        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                //  _context.Students.Remove(student);دیگخ rwmoveنداریمبخاطر انی کار را کردیم یعنی نمیخوایم واقعا خذف شود فقط این فرد غیرفعال بشود
                student.IsActive = false;
                _context.SaveChanges();
            }
        }
        public Student? GetById(int id)
        {
            return _context.Students.Include(s => s.Grade)
        .FirstOrDefault(s => s.Id == id);
        }
        public  bool NationalCodeExists(string nationalCode)
        {
            return _context.Students
                           .Any(s => s.NationalCode == nationalCode);
        }
        public void Restore(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                student.IsActive = true;

                _context.SaveChanges();
            }
        }
        public IQueryable<Student> GetInactiveStudents()
        {
            return _context.Students
                           .Where(student => !student.IsActive);
        }
    }
}
