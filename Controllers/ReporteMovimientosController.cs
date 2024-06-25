using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Parqueadero.Datos;
using Parqueadero.Models;
using OfficeOpenXml; // Asegúrate de tener EPPlus instalado
using OfficeOpenXml.Table; // Importar las tablas de EPPlus
using System.Collections.Generic;
using System.Linq;

namespace Parqueadero.Controllers
{
    public class ReporteMovimientosController : Controller
    {
        private readonly MovimientoDatos _movimientoDatos;
        private readonly UsuarioDatos _usuarioDatos;

        public ReporteMovimientosController()
        {
            _movimientoDatos = new MovimientoDatos();
            _usuarioDatos = new UsuarioDatos();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ReportParametersModel
            {
                Users = _usuarioDatos.Listar()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GenerateReport(ReportParametersModel parameters)
        {
            var reportData = _movimientoDatos.ObtenerMovimientosPorParametros(parameters);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Generar archivo Excel
            var stream = new MemoryStream();
            using (var paquete = new ExcelPackage(stream))
            {
                var worksheet = paquete.Workbook.Worksheets.Add("Movimientos");
                worksheet.Cells.LoadFromCollection(reportData, true, TableStyles.Medium2);
                paquete.Save();
            }
            stream.Position = 0;
            var fileName = $"ReporteMovimientos_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, fileName);
        }

    }
}

