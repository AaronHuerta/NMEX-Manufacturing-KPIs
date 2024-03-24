using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_User
{
    public class Register
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastNamePaternal { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastNameMaternal { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool Privilege { get; set; }

        public int Plant_id { get; set; }

    }
}
