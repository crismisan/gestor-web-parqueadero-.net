using Microsoft.AspNetCore.Mvc;
using Parqueadero;
using Parqueadero.Models;
using Parqueadero.Datos;
using System.Data.SqlClient;
using System.Data;

namespace Parqueadero.Controllers
{
    public class EntregaController : Controller
    {
        private EntregaDatos _entregaParqueaderoDatos = new EntregaDatos();
        private UsuarioDatos _usuarioDatos = new UsuarioDatos();
        private EmpleadoDatos _empleadoDatos = new EmpleadoDatos();
        private CuposDatos _cupoDatos = new CuposDatos();
        private VehiculosDatos _vehiculoDatos = new VehiculosDatos();
        private AsignacionDatos _asignacionDatos = new AsignacionDatos();
        public IActionResult ListarEntrega()
        {
            var oEntrega = _entregaParqueaderoDatos.Listar();
            return View(oEntrega);
        }

        public IActionResult AgregarEntrega()
        {
            var model = new EntregaModel
            {
                AsignacionesDisponibles = _asignacionDatos.Listar()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AgregarEntrega(EntregaModel oEntrega)
        {

            var respuesta = _entregaParqueaderoDatos.InsertarEntrega(oEntrega);
            if (respuesta)
            {
                var ListaEntrega = _entregaParqueaderoDatos.Listar();
                return View("ListarEntrega", ListaEntrega);
            }
            else
            {
                oEntrega.AsignacionesDisponibles = _asignacionDatos.Listar();
                return View(oEntrega);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerDatosAsignacion(int idAsignacion)
        {
            var datosAsignacion = _asignacionDatos.Consultar(idAsignacion);
            if (datosAsignacion == null)
            {
                return NotFound();
            }
            return Json(new
            {
                idUsuario = datosAsignacion.IdUsuario,
                idEmpleado = datosAsignacion.IdEmpleado,
                idCupo = datosAsignacion.IdCupo,
                idVehiculo = datosAsignacion.IdVehiculo
            });


        }
    }
}
