using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SBS.ApplicationCore.DTO;
using SBS.ApplicationCore.Entities;
using SBS.ApplicationCore.Interfaces.Services;
using SBS.Web.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace SBS.Web.Controllers
{
    public class UsuarioController : BaseController
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
            IUsuarioService usuarioService,
            IOptions<AppSettingsSection.AppSettings> appSettings
            ) : base(
                  appSettings
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
        [HttpPost("Buscar")]
        public JsonResult BuscarUsuario([FromBody] FiltroBusquedaUsuarioDto param)
        {
            var success = true;
            IEnumerable<BusquedaUsuarioDto> lista = null;

            try
            {
                lista = _usuarioService.BuscarUsuario(param).Result;

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

        /// <summary>
        /// GrabarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Grabar")]
        public JsonResult GrabarUsuario([FromBody] Usuario param)
        {
            var success = true;
            int result = 0;

            try
            {
                result = _usuarioService.GrabarUsuario(param).Result;
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(default(EventId), ex, ex.Message);
            }

            return Json(new
            {
                success,
                data = result
            });
        }

        /// <summary>
        /// EliminarUsuario
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Eliminar")]
        public JsonResult EliminarUsuario([FromBody] Usuario param)
        {
            var success = true;
            int result = 0;

            try
            {
                result = _usuarioService.EliminarUsuario(param.UsuarioId).Result;
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(default(EventId), ex, ex.Message);
            }

            return Json(new
            {
                success,
                data = result
            });
        }



        /// <summary>
        /// ExportarExcel
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("ExportarExcel")]
        public FileResult ExportarExcel([FromBody] FiltroBusquedaUsuarioDto param)
        {
            
            IEnumerable<BusquedaUsuarioDto> lista = null;

            lista = _usuarioService.BuscarUsuario(param).Result;

            return ExportExcel(lista, @"wwwroot\FilesDownload\ExcelFiles\", "curva_historica");
            
        }


        private FileResult ExportExcel(IEnumerable<BusquedaUsuarioDto> data, string outputDirectory, string newFileName)
        {
            // Instantiate a workbook
            Workbook wb = new Workbook();

            // Access first worksheet
            Worksheet worksheet = wb.Worksheets[0];

            int nroLinea = 0;
            // columnas
            int columnaFechaProceso = 0;
            int columnaTipoCurva = 1;
            int columnaPlazo = 2;
            int columnaTasa = 3;

            // Set columns title
            // 1ra línea
            worksheet.Cells[nroLinea, 0].Value = "Rango de Fechas del ";
            worksheet.Cells[nroLinea, 1].Value = "04/06/2020";
            worksheet.Cells[nroLinea, 2].Value = "al ";
            worksheet.Cells[nroLinea, 3].Value = "04/06/2020";

            nroLinea++;

            // 2da línea
            worksheet.Cells[nroLinea, columnaFechaProceso].Value = "Fecha de Proceso";
            worksheet.Cells[nroLinea, columnaTipoCurva].Value = "Tipo de Curva";
            worksheet.Cells[nroLinea, columnaPlazo].Value = "Plazo (DIAS)";
            worksheet.Cells[nroLinea, columnaTasa].Value = "Tasas (%)";

            nroLinea++;

            // Fill data
            foreach (BusquedaUsuarioDto item in data)
            {
                worksheet.Cells[nroLinea, columnaFechaProceso].Value = "04/06/2020";
                worksheet.Cells[nroLinea, columnaTipoCurva].Value = "Tipo Curva 1";
                worksheet.Cells[nroLinea, columnaPlazo].Value = 1;
                worksheet.Cells[nroLinea, columnaTasa].Value = 0;

                nroLinea++;
            }

            // Save the workbook
            string outputDir = Path.GetFullPath(outputDirectory);
            string fileName = string.Concat(outputDir, newFileName, ".xlsx");

            if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

            wb.Save(fileName, Aspose.Cells.SaveFormat.Xlsx);

            var myfile = System.IO.File.ReadAllBytes(fileName);
            var excelFile = new FileContentResult(myfile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = newFileName
            };

            if (System.IO.File.Exists(fileName)) System.IO.File.Delete(fileName);

            return excelFile;

        }

    }

}

