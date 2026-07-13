using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;


namespace SchoolManagementSystem.Controllers
{

    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;
        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index(string ? search,string? sortOrder)
        {
            // return Content("Student Controller Works");
            // var students = _context.Students.ToList();
            var students = _context.Students.AsQueryable ();
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
            ViewBag.NameSortParm =
     sortOrder == "name" ? "name_desc" : "name";
            return View(students.ToList());
        }
        [HttpGet]
        public IActionResult creat()
        {
            return View();

        }
        private bool NationalCodeExists(string nationalCode)
        {
            return _context.Students
                           .Any(s => s.NationalCode == nationalCode);
        }

        [HttpPost]
        public IActionResult Creat(Student student)
        {
            //Console.WriteLine(student.FirstName);برای اینکه ببینم مقدار میگیرند یانه بریک پوینت نیذارین و اجره
            // Console.WriteLine(student.LastName);
            if (!ModelState.IsValid)
                return View(student);

            if (NationalCodeExists(student.NationalCode))
            {
                ModelState.AddModelError("NationalCode",
                    "این کد ملی قبلاً ثبت شده است.");

                return View(student);
            }
            student.RegisterDate = DateTime.Now;
            student.IsActive = true;
            _context.Students.Add(student);
            //student.RegisterDate = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
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
            _context.Students.Update(student);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delet(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        //چون تابع دیلیت فقط ای دی را برای حفظ میگیرد پس برا یاینکه خطا ندهد این خظ را اضاففه میکینم و به اینصورت مینویسیم 
      //  [ActionName("Delet")]
      //  public IActionResult DeletcoDeleteConfirmed(int id)
        [HttpPost]
        [ActionName("Delet")]
        public IActionResult DeletcoDeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            
                return NotFound();
            _context.Students.Remove(student);
            _context.SaveChanges();
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

        
