using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using SchoolManagementSystem.ViewModels.Students;

using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Services.Interfaces;

using SchoolManagementSystem.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SchoolManagementSystem.Controllers
{

    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;
        // private readonly IStudentRepository _studentRepository;این جای خودش به سرویس داد
        private readonly IStudentService _studentService;
        private void FillDropDowns(EditStudentViewModel model)
        {
            model.GenderList = new List<SelectListItem>
    {
        new SelectListItem
        {
            Text = "مرد",
            Value = ((int)Gender.Male).ToString()
        },
        new SelectListItem
        {
            Text = "زن",
            Value = ((int)Gender.Female).ToString()
        }
    };

            model.GradeList = new List<SelectListItem>
    {
        new SelectListItem { Text = "اول ابتدایی", Value = "1" },
        new SelectListItem { Text = "دوم ابتدایی", Value = "2" },
        new SelectListItem { Text = "سوم ابتدایی", Value = "3" },
        new SelectListItem { Text = "چهارم ابتدایی", Value = "4" },
        new SelectListItem { Text = "پنجم ابتدایی", Value = "5" },
        new SelectListItem { Text = "ششم ابتدایی", Value = "6" }
    };
        }
        /* public StudentController(SchoolDbContext context)حالا که ریپ.زیتوری ساختین اینو نمیخواییم
         {
             _context = context;
         }*/
        //این خطوط پایین ریپ.زیتوری ن
        /*  public StudentController( SchoolDbContext context,
        IStudentRepository studentRepository)
          {
             // _context = context;
              _studentRepository = studentRepository;
          }*/

        public StudentController(SchoolDbContext context,IStudentService studentService)
        {
            // _context = context;
            _studentService = studentService;
        }


        [HttpGet]
        public IActionResult Index(int? selectedId,string ? search,string? sortOrder,int page=1)
        {
            // return Content("Student Controller Works");
            // var students = _context.Students.ToList();
          //  var students = _context.Students.AsQueryable ();
          //چون داریم از ریپوزیتوری استفاده میکنیم خط بالا حذف و پایینی اضافه 

            var students = _studentService.GetAll();
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
                SelectedId = selectedId,
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
            // return View(new CreateStudentViewModel());چون آمدیم از حالت دراپ دون برای جنسیت استفاده کنیم این را دیگه نمینویسم بجاش خطوط زی را داریم
            var model = new CreateStudentViewModel();

            model.GenderList = new List<SelectListItem>
{
    new SelectListItem
    {
        Text="مرد",
        Value=((int)Gender.Male).ToString()
    },

    new SelectListItem
    {
        Text="زن",
        Value=((int)Gender.Female).ToString()
    }
};
            model.GradeList = new List<SelectListItem>
{
    new SelectListItem { Text="اول ابتدایی", Value="1" },
    new SelectListItem { Text="دوم ابتدایی", Value="2" },
    new SelectListItem { Text="سوم ابتدایی", Value="3" },
    new SelectListItem { Text="چهارم ابتدایی", Value="4" },
    new SelectListItem { Text="پنجم ابتدایی", Value="5" },
    new SelectListItem { Text="ششم ابتدایی", Value="6" }
};
            return View(model);
        }
     /*   private bool NationalCodeExists(string nationalCode) رفت داخل ریپوزیتوری
        {
            return _context.Students
                           .Any(s => s.NationalCode == nationalCode);
        }*/


        [HttpPost]
        public IActionResult Create(CreateStudentViewModel model)

        {
            
            //Console.WriteLine(student.FirstName);برای اینکه ببینم مقدار میگیرند یانه بریک پوینت نیذارین و اجره
            // Console.WriteLine(student.LastName);
            if (!ModelState.IsValid)
                return View(model);
            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalCode = model.NationalCode,
                BirthDate = model.BirthDate,
                FatherName = model.FatherName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Grade = model.Grade
            };
            var result =_studentService.Add(student);
            if (!result.Success)
            {
               ModelState.AddModelError("NationalCode", result.Message);
                 return View(model);}
                // if (NationalCodeExists(student.NationalCode))بخاطر ریپوزیتوری جابجا با خط پایین
                /*    if (_studentService. NationalCodeExists(student.NationalCode))بخاطر سرویس تغییر میکنند
                {
                    ModelState.AddModelError("NationalCode",
                        "این کد ملی قبلاً ثبت شده است.");

                    return View(student);
                }*/
                // student.RegisterDate = DateTime.Now; این دوتا رفتن داخل سرویس نباید مداخل کنترلر باشن
                //student.IsActive = true;
                _studentService.Add(student);
            // _context.Students.Add(student);
            //student.RegisterDate = DateTime.Now;
            // _context.SaveChanges();
            //برای نمایش پیام موفقیت آمیز بودن ثبت دانش آموز
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
            

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {


            //var student = _context.Students.Find(id);بخاطر ریپوزیتوری حذف با پایینی
             var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            var model = new EditStudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                NationalCode = student.NationalCode,
                BirthDate = student.BirthDate,
                FatherName = student.FatherName,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Grade = student.Grade
            };

            FillDropDowns(model);
            return View(model);
        }
        // return View(student);بخاطر ویو مدلاین حذف میشه



        [HttpPost]

        public IActionResult Edit(EditStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                FillDropDowns(model);
                    return View(model);
            }
               
            var student = new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalCode = model.NationalCode,
                BirthDate = model.BirthDate,
                FatherName = model.FatherName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Grade = model.Grade
            };
            //  _context.Students.Update(student);  //بخاطر دستورات ریپوزیتوری اینا حذف میشوند
            //  _context.SaveChanges();
            _studentService.Update(student);

            TempData["SuccessMessage"] = "اطلاعات دانش‌آموز با موفقیت ویرایش شد.";

            return RedirectToAction(nameof(Index), new { selectedId = student.Id });
        }
        [HttpGet]
        public IActionResult Delete (int id)
        {  //var student = _context.Students.Find(id);بخاطر ریپوزیتوری حذف با پایینی
            var student = _studentService.GetById(id);
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
            var student = _studentService.GetById(id);
            if (student == null)
            
                return NotFound();
            //  _context.Students.Remove(student);
            //_context.SaveChanges();
            _studentService.Delete(student);
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

        
