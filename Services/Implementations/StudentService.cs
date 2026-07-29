using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Services.Interfaces;
using SchoolManagementSystem.shared;

namespace SchoolManagementSystem.Services.Implementations
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IQueryable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public ServiceResult Add(Student student)
        {
            if (_studentRepository.NationalCodeExists(student.NationalCode))
            {

                return new ServiceResult
                {
                    Success = false,
                    Message = "این کد ملی قبلاً ثبت شده است."
                };
                    
            }
            student.RegisterDate = DateTime.Now;
            student.IsActive = true;
            _studentRepository.Add(student);
            return new ServiceResult
            {
                Message = "با موفقیت اصافه شد.",
                Success = true
               
            };
        }

        public void Update(Student student)
        {
            _studentRepository.Update(student);
        }

        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }

        public bool NationalCodeExists(string nationalCode)
        {
            return _studentRepository.NationalCodeExists(nationalCode);
        }

    }
}
