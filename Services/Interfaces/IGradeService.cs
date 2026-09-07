using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolManagementSystem.Services.Interfaces
{
    public interface IGradeService
    {
        List<SelectListItem> GetGradeDropDown();
    }
}