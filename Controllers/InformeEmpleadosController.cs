using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Parqueadero.Datos;

namespace Parqueadero.Controllers
{
    public class InformeEmpleadosController : Controller
    {
        private readonly EmpleadoDatos _empleadoDatos;
        public InformeEmpleadosController()
        {
            _empleadoDatos = new EmpleadoDatos();   
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerarReporte()
        {
            var empleados = _empleadoDatos.Listar();

            // Configurar contexto de licencia
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // aca se genera un archivo Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("InformeEmpleados");

                
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "Cargo";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Telefono";
                

                // se aplican estilos
                using (var range = worksheet.Cells["A1:E1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                
                for (int i = 0; i < empleados.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = empleados[i].Idempleado;
                    worksheet.Cells[i + 2, 2].Value = empleados[i].Nombre;
                    worksheet.Cells[i + 2, 3].Value = empleados[i].Cargo;
                    worksheet.Cells[i + 2, 4].Value = empleados[i].Email;
                    worksheet.Cells[i + 2, 5].Value = empleados[i].Telefono;
                    
                }

                // Autoajustar columnas
                worksheet.Cells.AutoFitColumns();

                // Guardar y devolver el archivo Excel
                var stream = new MemoryStream(package.GetAsByteArray());
                var fileName = $"InformeEmpleados_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
        }
    }
}
