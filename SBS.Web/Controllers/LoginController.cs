using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SBS.ApplicationCore.Entities;
using SBS.Web.Models;
using SBS.Web.Models.Login;
using SBS.Web.Utils;
using SBS.Web.Utils.Extensions;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SBS.Web.Controllers
{
    public class LoginController : Controller
    {
        #region Propiedades
        // private readonly IUsuarioProvider _usuarioProvider;
        public readonly IOptions<AppSettingsSection.AppSettings> _appSettings;

        #endregion

        #region Constructor

        public LoginController
        (
            // IUsuarioProvider usuarioProvider,
            IOptions<AppSettingsSection.AppSettings> appSettings
            )
        {
            // _usuarioProvider = usuarioProvider;
            _appSettings = appSettings;
        }

        #endregion

        #region Acciones

        #region Login 

        public IActionResult Index(string returnUrl = null)
        {

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var validaLogin = await _usuarioProvider.ValidarLogin(model.CodigoUsuario, model.ClaveSecreta) ?? new ValidaLoginModel();
                    ValidaLoginModel validaLogin = null;
                    if (model.CodigoUsuario == "gmatos" && model.ClaveSecreta == "123456")
                    {
                        validaLogin = new ValidaLoginModel
                        {
                            Result = 1,
                            Mensaje = "",
                            CodigoUsuario = model.CodigoUsuario
                        };

                    } else
                    {
                        validaLogin = new ValidaLoginModel
                        {
                            Mensaje = "Usuario o contraseña incorrectos"
                        };
                    }

                    if (validaLogin != null && validaLogin.Result == Constantes.LoginResultado.UsuarioValido)
                    {
                        //var usuario = await _usuarioProvider.ObtenerDatosSesion(validaLogin.CodigoUsuario);
                        var usuario = new Usuario
                        {
                            CodigoUsuario = model.CodigoUsuario,
                            Email = "guido.matos.88@gmail.com",
                            ApellidoPaterno = "Matos",
                            ApellidoMaterno = "Camones",
                            PrimerNombre = "Guido",
                            SegundoNombre = "Alan",
                            Alias = "Guido Matos"
                        };

                        await GuardarClaimsSesion(usuario);
                       
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }

                    var mensajeError = string.IsNullOrEmpty(validaLogin.Mensaje) ? "Error al procesar la solicitud" : validaLogin.Mensaje;
                    TempData["ErrorLogin"] = mensajeError;
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception ex)
                {
                    TempData["errorLogin"] = ex.Message;
                    return RedirectToAction("Index", "Login");
                }
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

        #endregion
        
        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }


        #endregion

        private async Task GuardarClaimsSesion(Usuario usuario)
        {
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, usuario.CodigoUsuario)
                };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            await HttpContext.Session.SetData(Constantes.Session.UsuarioActual, usuario);
        }
    }
}