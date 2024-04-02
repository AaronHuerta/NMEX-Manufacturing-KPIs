using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class CategoryCreationViewModel:Categories
    {
        public IEnumerable<SelectListItem> Function_names { get; set; }
    }
}
