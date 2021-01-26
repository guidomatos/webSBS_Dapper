using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBS.Web.Utils;
using SBS.Web.Utils.Extensions;
using SBS.ApplicationCore.Entities;

namespace SBS.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        public readonly IOptions<AppSettingsSection.AppSettings> _appSettings;
        // public readonly IUsuarioProvider _usuarioProvider;

        public BaseController
        (
        // IUsuarioProvider usuarioProvider,
        IOptions<AppSettingsSection.AppSettings> appSettings
        )
        {
            _appSettings = appSettings;
            // _usuarioProvider = usuarioProvider;
        }

        #region Propiedades

        protected Usuario UsuarioActual { get; private set; }

        #endregion

        #region Overrides

        #region Para cuando sea ADFS

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UsuarioActual = ObtenerUsuarioActual().Result;
            if (UsuarioActual == null)
            {
                HttpContext.Session.Clear();
                context.Result = new RedirectResult("/Login/LogOut");
                return Task.FromResult(0);
            }

            bool isAjaxCall = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjaxCall)
            {
                base.OnActionExecutionAsync(context, next);
                return Task.FromResult(0);
            }

            CargarViewBag(UsuarioActual);

            return base.OnActionExecutionAsync(context, next);
        }

        #endregion

        #endregion

        #region Metodos Privados
        private async Task<Usuario> ObtenerUsuarioActual()
        {
            return await GetSession<Usuario>(Constantes.Session.UsuarioActual);
        }
        private void CargarViewBag(Usuario usuarioActual)
        {
            ViewBag.NombreUsuarioBienvenida = usuarioActual.Alias;
            ViewBag.NombreUsuario = usuarioActual.PrimerNombre + " " + usuarioActual.ApellidoPaterno;

        }
        #endregion

        #region Metodos Publicos

        private async Task<T> GetSession<T>(string key)
        {
            return await HttpContext.Session.GetData<T>(key);
        }

        private async Task SetSession(string key, object value)
        {
            await HttpContext.Session.SetData(key, value);
        }

        #endregion
    }
}