using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBS.Web.Models.Login
{
    public class LoginClaveModel
    {
        public string CodigoUsuario { get; set; }

        [Required(ErrorMessage = "La Clave Secreta es Requerida")]
        [DataType(DataType.Password)]
        public string ClaveSecreta { get; set; }

        [Required(ErrorMessage = "La Nueva Clave es Requerida")]
        [DataType(DataType.Password)]
        public string NuevaClave { get; set; }

        [Required(ErrorMessage = "Confirmar Nueva Clave es Requerida")]
        [Compare(nameof(NuevaClave), ErrorMessage = "La nueva clave no es igual")]
        [DataType(DataType.Password)]
        public string ConfirmaNuevaClave { get; set; }
    }
}
