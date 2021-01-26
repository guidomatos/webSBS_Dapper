using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBS.Web.Utils
{
    public static class Constantes
    {
        public const string FormatoFechaES = "dd/MM/yyyy";
        public const string FormatoFechaEN = "yyyy-MM-dd";
        public static class LoginResultado
        {
            public const int UsuarioInValido = 0;
            public const int UsuarioValido = 1;
        }
        public static class Session
        {
            public const string UsuarioActual = "UsuarioActual";
        }
    }
}