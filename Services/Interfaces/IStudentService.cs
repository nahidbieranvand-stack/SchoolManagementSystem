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
      ServiceResult  Add(Student student);

        void Update(Student student);

        void Delete(int id);

        bool NationalCodeExists(string nationalCode);
        
    }
}
