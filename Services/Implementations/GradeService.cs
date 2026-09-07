using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Services.Interfaces;

namespace SchoolManagementSystem.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<SelectListItem> GetGradeDropDown()
        {
            return _gradeRepository.GetAll()
                .Where(g => g.IsActive)
                .OrderBy(g => g.Order)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToList();
        }
    }
}