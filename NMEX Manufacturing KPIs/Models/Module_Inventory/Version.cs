using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class Version
    {
        public int Version_id { get; set; }
        [StringLength(maximumLength: 50,ErrorMessage = "VALUE MUST NOT EXCEED 50 CHARACTERS")]
        public string Version_description { get; set; }
        public bool Active { get; set; }
        public DateTime EndOfSupport { get; set; }
    }
}
