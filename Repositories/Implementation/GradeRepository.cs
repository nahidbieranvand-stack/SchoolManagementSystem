using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Services.Interfaces;
namespace SchoolManagementSystem.Repositories.Implementation
{
    public class GradeRepository:IGradeRepository
    {
        private readonly SchoolDbContext _context;

        public GradeRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public List<Grade> GetAll()
        {
            return _context.Grades.ToList();
        }
    }
}
