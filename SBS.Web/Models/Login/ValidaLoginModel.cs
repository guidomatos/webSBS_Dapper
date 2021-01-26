using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBS.Web.Models.Login
{
    public class ValidaLoginModel
    {
        public int Result { get; set; }
        public string Mensaje { get; set; }
        public string CodigoUsuario { get; set; }
    }
}
