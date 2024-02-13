
using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Persona")]
        [Required(ErrorMessage ="EL CAMPO {0} ES REQUERIDO")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "EL CAMPO {0} ES REQUERIDO")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "EL CAMPO {0} ES REQUERIDO")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Edad de persona")]
        [StringLength(maximumLength: 3, ErrorMessage = "EL CAMPO {0} DEBE SER MENOR A 3 CARACTERES")]
        [Required(ErrorMessage = "EL CAMPO {0} ES REQUERIDO")]
        public string Edad { get; set; }

    }
}
