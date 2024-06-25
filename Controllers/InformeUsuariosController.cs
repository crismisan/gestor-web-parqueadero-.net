using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table; // Importar las tablas de EPPlus
using Parqueadero.Datos;

namespace Parqueadero.Controllers
{
    public class InformeUsuariosController : Controller
    {
        private readonly UsuarioDatos _usuarioDatos;

        public InformeUsuariosController()
        {
            _usuarioDatos = new UsuarioDatos();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerarReporte()
        {
            var usuarios = _usuarioDatos.Listar();

            // Configurar contexto de licencia
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // se genera un archivo Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("InformeUsuarios");

                
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "Telefono";
                worksheet.Cells[1, 4].Value = "Correo";
                
                // se aplican estilos
                using (var range = worksheet.Cells["A1:E1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                for (int i = 0; i < usuarios.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = usuarios[i].Idusuario;
                    worksheet.Cells[i + 2, 2].Value = usuarios[i].Nombre;
                    worksheet.Cells[i + 2, 3].Value = usuarios[i].Telefono;
                    worksheet.Cells[i + 2, 4].Value = usuarios[i].Email;
                }

                // Autoajustar columnas
                worksheet.Cells.AutoFitColumns();

                // Guardar y devolver el archivo Excel
                var stream = new MemoryStream(package.GetAsByteArray());
                var fileName = $"InformeUsuarios_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
        }
    }
}
