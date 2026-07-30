using SchoolManagementSystem.Models;
namespace SchoolManagementSystem.Repositories.Interfaces
{
    public interface  IStudentRepository
    {
       IQueryable <Student> GetAll();
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
        bool NationalCodeExists(string nationalCode);
        Student? GetById(int id);
        void Restore(int id);
        IQueryable<Student> GetInactiveStudents();
    }
}
