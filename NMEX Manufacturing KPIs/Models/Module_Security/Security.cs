using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class Security
    {
        // Propiedades de la tabla Security
        public int Security_id { get; set; }
        public int SubCategory_id { get; set; }
        public int Plant_id { get; set; }
        public bool Result { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        

    }
}
