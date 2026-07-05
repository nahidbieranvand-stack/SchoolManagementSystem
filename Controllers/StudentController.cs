using Microsoft.AspNetCore.Mvc;


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
        public IActionResult Index()
        {
           // return Content("Student Controller Works");
            var students=_context.Students.ToList();
            return View(students);
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
           var student=_context.Students.Find(id);
            if (student == null){
                return NotFound();
            }  return View(student);     
        

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

    }
         

    }


//if (ModelState.IsValid)
           // {
              //  _context.Students.Add(student);
//_context.SaveChanges();
               // return RedirectToAction(nameof(Index));
//}
          //  return View(student);

        
