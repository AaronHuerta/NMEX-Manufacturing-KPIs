using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class SecurityCreationViewModel:Security
    {
        // Propiedades de la tabla Security

        public IEnumerable<SelectListItem> DeviceTypes { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Versions { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        //Filter by plant
        public IEnumerable<SelectListItem> Plants { get; set; }


    }
}
