using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class SubCategoryCreationViewModel : SubCategory
    {
        

        // Propiedad adicional para almacenar el id de la categoría
        public int Category_id { get; set; }
    }
}
