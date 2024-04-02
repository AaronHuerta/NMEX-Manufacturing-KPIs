
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class SubCategory
    {
        public int SubCategory_id { get; set; }

        public string SubCategory_description { get; set;}

        public bool Active { get; set;}

        public int Category_id { get; set; }


        //Atributos de SubCategories

        public string Category_description { get; set; }

        public string Category_name { get; set; }

        public string Result { get; set; }

        public string Comment { get; set; }

        public string UpdatedResult { get; set; }

        public string Function_name { get; set; }

        public string Plant_description { get; set; }

        public List<SubCategory> SubCategories { get; set; }
        public string[] Files { get; set; }

    }
}
