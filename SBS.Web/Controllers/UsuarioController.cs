using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBS.ApplicationCore.Entities;
using SBS.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace SBS.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<Controller> _logger;
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// UsuarioController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="usuarioService"></param>
        public UsuarioController
            (
            ILogger<Controller> logger,
            IUsuarioService usuarioService
            )
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// BuscarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("BuscarUsuario")]
        public JsonResult BuscarUsuario()
        {
            var success = true;
            IEnumerable<Usuario> lista = null;

            try
            {
                lista = _usuarioService.BuscarUsuario().Result;
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(default(EventId), ex, ex.Message);
            }

            return Json(new
            {
                success,
                data = lista
            });
        }

    }
}
