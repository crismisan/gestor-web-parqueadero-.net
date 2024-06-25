using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;
namespace Parqueadero.Controllers
{
    public class MovimientosController : Controller
    {

        private readonly MovimientoDatos _MovimientosDatos = new MovimientoDatos();
        private readonly UsuarioDatos _usuarioDatos = new UsuarioDatos();
        private readonly EmpleadoDatos _empleadoDatos = new EmpleadoDatos();
        private readonly CuposDatos _cupoDatos = new CuposDatos();
        private readonly VehiculosDatos _vehiculoDatos = new VehiculosDatos();
        public IActionResult ListarMovimientos()
        {
            var oMovimientos = _MovimientosDatos.ListarMovimientos();
            return View(oMovimientos);
        }

        public IActionResult GuardarMovimientos()
        {
            var model = new MovimientosModel
            {
                // Rellenar listas de usuarios, empleados, cupos, vehículos para los dropdowns
                Usuarios = _usuarioDatos.Listar(),
                Empleados = _empleadoDatos.Listar(),
                Cupos = _cupoDatos.Listar(),
                Vehiculos = _vehiculoDatos.Listar()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GuardarMovimientos(MovimientosModel oMovimientos)
        {


            var respuesta = _MovimientosDatos.GuardarMovimientos(oMovimientos);
            if (respuesta)
            {
                var listaMovimientos = _MovimientosDatos.ListarMovimientos();
                return View("ListarMovimientos", listaMovimientos);
            }
            else
            {
                return View();
            }
            return View();

        }

        public IActionResult EditarMovimientos(int Idmovimiento) {

            var Movimientos = _MovimientosDatos.ObtenerMovimientos(Idmovimiento);
            if (Movimientos == null)
            {
                return NotFound();
            }
            // Rellenar listas de usuarios, empleados, cupos, vehículos para los dropdowns
            Movimientos.Usuarios = _usuarioDatos.Listar();
            Movimientos.Empleados = _empleadoDatos.Listar();
            Movimientos.Cupos = _cupoDatos.Listar();
            Movimientos.Vehiculos = _vehiculoDatos.Listar();
            
            return View(Movimientos);
        }

        [HttpPost]
        public IActionResult EditarMovimientos(MovimientosModel Movimientos)
        {


            var respuesta = _MovimientosDatos.EditarMovimientos(Movimientos);
            if (respuesta)
            {
                var Listamovimientos = _MovimientosDatos.ListarMovimientos();
                return View("ListarMovimientos", Listamovimientos);
            }
                
            else
                return View();
            return View();

        }

        public IActionResult EliminarMovimientos(int Idmovimiento)
        {

            var omovimientos = _MovimientosDatos.ObtenerMovimientos(Idmovimiento);

            return View(omovimientos);
        }

        [HttpPost]
        public IActionResult EliminarMovimientos(MovimientosModel oMovimientos)
        {

            var respuesta = _MovimientosDatos.EliminarMovimientos(oMovimientos.Idmovimiento);
            if (respuesta)
                return RedirectToAction("ListarMovimientos");
            else
                return View();
            return View();
        }

        public IActionResult GraficaMovimientos()
        {
            var movimientos = _MovimientosDatos.ListarMovimientos();

            // se agrupa por fecha y se cuentan los movimientos
            var datosGrafica = movimientos
                .GroupBy(m => m.FechaHora.Date)
                .Select(g => new { Fecha = g.Key.ToShortDateString(), Cantidad = g.Count() })
                .ToList();

            ViewBag.Fechas = datosGrafica.Select(d => d.Fecha).ToArray();
            ViewBag.Cantidades = datosGrafica.Select(d => d.Cantidad).ToArray();

            return View();
        }


    }
}
