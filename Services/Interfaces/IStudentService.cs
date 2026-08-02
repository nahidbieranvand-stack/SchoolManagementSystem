using SchoolManagementSystem.shared;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services.Implementations;

namespace SchoolManagementSystem.Services.Interfaces
{
    public interface  IStudentService
    {
        IQueryable<Student> GetAll();

        Student? GetById(int id);

        //   void Add(Student student);با ایجاد سرویس این خط از بین میرود
      ServiceResult  Add(Student student, IFormFile? imageFil);

        ServiceResult Update(Student student, IFormFile? imageFile);

        void Delete(int id);

        bool NationalCodeExists(string nationalCode);
        void Restore(int id);
        IQueryable<Student> GetInactiveStudents();

    }
}
