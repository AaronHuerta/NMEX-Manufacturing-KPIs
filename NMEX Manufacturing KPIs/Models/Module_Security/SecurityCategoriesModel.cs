using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class SecurityCategoriesModel
    {
        // Propiedades de la tabla Security
        public int Security_id { get; set; }
        public int SubCategory_id { get; set; }
        public int Plant_id { get; set; }
        public bool Result { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }

        // Propiedades de la tabla Categories
        public int Category_id { get; set; }
        public string Category_name { get; set; }
        public string Category_description { get; set; }
        public int Function_id { get; set; }
        public bool CategoriesActive { get; set; }
        public string Category_identifier { get; set; }

        // Propiedades de la tabla Function
        public string Function_name { get; set; }
    }
}
