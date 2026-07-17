using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;

using SchoolManagementSystem.Repositories.Interfaces;
namespace SchoolManagementSystem.Controllers
{

    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IStudentRepository _studentRepository;

        /* public StudentController(SchoolDbContext context)حالا که ریپ.زیتوری ساختین اینو نمیخواییم
         {
             _context = context;
         }*/
        //این خطوط پایین ریپ.زیتوری ن
        public StudentController( SchoolDbContext context,
      IStudentRepository studentRepository)
        {
           // _context = context;
            _studentRepository = studentRepository;
        }



        [HttpGet]
        public IActionResult Index(string ? search,string? sortOrder,int page=1)
        {
            // return Content("Student Controller Works");
            // var students = _context.Students.ToList();
          //  var students = _context.Students.AsQueryable ();
          //چون داریم از ریپوزیتوری استفاده میکنیم خط بالا حذف و پایینی اضافه 
            var students = _studentRepository.GetAll();
            int pagesize = 10;//این تعدادانش آموزادرهر صفحه نمایش میدهد

           

            if (!string.IsNullOrWhiteSpace(search))
            {
                //اینجا با نامو نام خانوادگی وکد ملی سرچ میکنیم
                students = students.Where(s =>
                s.FirstName.Contains(search) ||
                s.LastName.Contains(search) ||
           //  s.NationalCode.Contains(sear ch)).ToList();این برای زمانی بود که فقط خواستیم جستجو کنیم
           s.NationalCode.Contains(search));

            }
            switch (sortOrder)
            {
                case "name":
                    students = students.OrderBy(s => s.FirstName);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;

                case "grade":
                    students = students.OrderBy(s => s.Grade);
                    break;

                default:
                    students = students.OrderBy(s => s.Id);
                    break;
            }
            //برای اینکه این حات برای مرتب سازی ها ایجاد کنیم که اگر نزولی بود بشود صعودی و برعمسنام ▼
            
            
            int totalStudents = students.Count();
            int totalPages = (int)Math.Ceiling((double)totalStudents / pagesize);
          //  ViewBag.TotalPages = totalPages;//این دو تا ی=برای ساخت دکمه های نکست و بک که مقدارشان را به ویو میفرستیم
          //  ViewBag.CurrentPage = page;
          //ViewBag.PageSize = pagesize;
          //  ViewBag.TotalStudents= totalStudents;
            int skip = (page - 1) * pagesize;//تعدااد رد شدن صفحه را نشان میدهد
            var pagestudents = students
                .Skip(skip)
                .Take(pagesize)
                .ToList();
            if (page < 1)
            {
                page = 1;
            }

            if (page > totalPages)
            {
                page = totalPages;
            }

            // return View(students.ToList());
            //  return View(pagestudents);//حالا کهپیج استودیونت رو نوشتیم بالایی رئ غیر فعال میکنیم
            var viewModel = new StudentListViewModel
            {
                Students = pagestudents,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pagesize,
                TotalStudents = totalStudents,
                Search = search,
                SortOrder = sortOrder,
                NameSortParm = sortOrder == "name" ? "name_desc" : "name"
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult create()
        {
            return View();

        }
     /*   private bool NationalCodeExists(string nationalCode) رفت داخل ریپوزیتوری
        {
            return _context.Students
                           .Any(s => s.NationalCode == nationalCode);
        }*/

        [HttpPost]
        public IActionResult Create(Student student)
        {
            //Console.WriteLine(student.FirstName);برای اینکه ببینم مقدار میگیرند یانه بریک پوینت نیذارین و اجره
            // Console.WriteLine(student.LastName);
            if (!ModelState.IsValid)
                return View(student);
           // if (NationalCodeExists(student.NationalCode))بخاطر ریپوزیتوری جابجا با خط پایین
                if (_studentRepository. NationalCodeExists(student.NationalCode))
            {
                ModelState.AddModelError("NationalCode",
                    "این کد ملی قبلاً ثبت شده است.");

                return View(student);
            }
            student.RegisterDate = DateTime.Now;
            student.IsActive = true;
            _studentRepository.Add(student);
           // _context.Students.Add(student);
            //student.RegisterDate = DateTime.Now;
           // _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {


            //var student = _context.Students.Find(id);بخاطر ریپوزیتوری حذف با پایینی
             var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);


        }
        [HttpPost]
        
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);
            //  _context.Students.Update(student);  //بخاطر دستورات ریپوزیتوری اینا حذف میشوند
           //  _context.SaveChanges();
           _studentRepository.Update(student);
           


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete (int id)
        {  //var student = _context.Students.Find(id);بخاطر ریپوزیتوری حذف با پایینی
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        //چون تابع دیلیت فقط ای دی را برای حفظ میگیرد پس برا یاینکه خطا ندهد این خظ را اضاففه میکینم و به اینصورت مینویسیم 
      //  [ActionName("Delete")]
      //  public IActionResult Deletepost(int id)
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Deletepost(int id)
        {
            //var student = _context.Students.Find(id);بخاطر ریپوزیتوری حذف با پایینی
            var student = _studentRepository.GetById(id);
            if (student == null)
            
                return NotFound();
            //  _context.Students.Remove(student);
            //_context.SaveChanges();
            _studentRepository.Delete(student);
            return RedirectToAction(nameof(Index));

           
        }




    }




}

//if (ModelState.IsValid)
           // {
              //  _context.Students.Add(student);
//_context.SaveChanges();
               // return RedirectToAction(nameof(Index));
//}
          //  return View(student);

        
