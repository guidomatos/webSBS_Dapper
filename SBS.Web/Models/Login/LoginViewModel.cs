using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBS.Web.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Código de Usuario es Requerido")]
        [MinLength(3, ErrorMessage = "El Código de Usuario debe ser mayor a 3 caracteres")]
        public string CodigoUsuario { get; set; }

        [Required(ErrorMessage = "La Clave Secreta es Requerida")]
        [DataType(DataType.Password)]
        public string ClaveSecreta { get; set; }

        public string ReturnUrl { get; set; }
    }
}
