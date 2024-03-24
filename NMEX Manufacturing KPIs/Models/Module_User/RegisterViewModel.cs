using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_User
{
    public class RegisterViewModel : Register
    {
        public IEnumerable<SelectListItem> Plants { get; set; }
    }
}
