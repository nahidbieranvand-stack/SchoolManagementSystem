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
    }
    
    
    
}
