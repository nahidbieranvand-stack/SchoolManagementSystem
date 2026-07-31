using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Services.Interfaces;
using SchoolManagementSystem.shared;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SchoolManagementSystem.Services.Implementations
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentService(IStudentRepository studentRepository,IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IQueryable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public ServiceResult Add(Student student, IFormFile? imageFile)
        {
            if (_studentRepository.NationalCodeExists(student.NationalCode))
            {

                return new ServiceResult
                {
                    Success = false,
                    Message = "این کد ملی قبلاً ثبت شده است."
                };
            }
                if (imageFile != null && imageFile.Length > 0)
{
    // تولید نام یکتای فایل
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

    // مسیر پوشه ذخیره عکس
    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "students");

    // اگر پوشه وجود نداشت، ایجاد شود
    if (!Directory.Exists(folderPath))
    {
        Directory.CreateDirectory(folderPath);
    }

    // مسیر کامل فایل
    var filePath = Path.Combine(folderPath, fileName);

    // ذخیره فایل
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        imageFile.CopyTo(stream);
    }

    // ذخیره مسیر در دیتابیس
    student.ImagePath = "/images/students/" + fileName;
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

        public void Update(Student student, IFormFile? imageFile)
        {
            
            if (imageFile != null && imageFile.Length > 0)
            {
                // تولید نام یکتای فایل
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // مسیر پوشه ذخیره عکس
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "students");

                // اگر پوشه وجود نداشت، ایجاد شود
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // مسیر کامل فایل
                var filePath = Path.Combine(folderPath, fileName);

                // ذخیره فایل
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // ذخیره مسیر در دیتابیس
                student.ImagePath = "/images/students/" + fileName;
            }
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
        public void Restore(int id)
        {
            _studentRepository.Restore(id);
        }
        public IQueryable<Student> GetInactiveStudents()
        {
            return _studentRepository.GetInactiveStudents();
        }

    }
}
