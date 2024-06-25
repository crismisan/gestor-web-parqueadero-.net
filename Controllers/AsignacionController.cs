using Microsoft.AspNetCore.Mvc;
using Parqueadero.Datos;
using Parqueadero.Models;

namespace Parqueadero.Controllers
{
    public class AsignacionController : Controller
    {
        private AsignacionDatos _asignacionParqueaderoDatos = new AsignacionDatos();
        private UsuarioDatos _usuarioDatos = new UsuarioDatos();
        private EmpleadoDatos _empleadoDatos = new EmpleadoDatos();
        private CuposDatos _cupoDatos = new CuposDatos();
        private VehiculosDatos _vehiculoDatos = new VehiculosDatos();
        public IActionResult ListarAsignaciones()
        {
            var Asignaciones = _asignacionParqueaderoDatos.Listar();
            return View(Asignaciones);
        }

        public IActionResult GuardarAsignacion()
        {
            var model = new AsignacionModel
            {
                Usuarios = _usuarioDatos.Listar(),
                Empleados = _empleadoDatos.Listar(),
                Cupos = _cupoDatos.Listar(),
                Vehiculos = _vehiculoDatos.Listar()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GuardarAsignacion(AsignacionModel asignacion)
        {

            var respuesta = _asignacionParqueaderoDatos.Insertar(asignacion);
            if (respuesta)
            {
                var listaAsignaciones = _asignacionParqueaderoDatos.Listar();
                return View("ListarAsignaciones", listaAsignaciones);
            }
            else
            {
                
                asignacion.Usuarios = _usuarioDatos.Listar();
                asignacion.Empleados = _empleadoDatos.Listar();
                asignacion.Cupos = _cupoDatos.Listar();
                asignacion.Vehiculos = _vehiculoDatos.Listar();
                return View(asignacion);
            }
        }
        public IActionResult EditarAsignacion(int idasignacion)
        {
            var asignacion = _asignacionParqueaderoDatos.Consultar(idasignacion);

            
            if (asignacion == null)
            {
                return NotFound();
            }

            asignacion.Usuarios = _usuarioDatos.Listar();
            asignacion.Empleados = _empleadoDatos.Listar();
            asignacion.Cupos = _cupoDatos.Listar();
            asignacion.Vehiculos = _vehiculoDatos.Listar();

            return View(asignacion);
        }

        [HttpPost]
        public IActionResult EditarAsignacion(AsignacionModel asignacion)
        {
            

            var respuesta = _asignacionParqueaderoDatos.Modificar(asignacion);
            if (respuesta)
            {
                var listaAsignaciones = _asignacionParqueaderoDatos.Listar();
                return View("ListarAsignaciones", listaAsignaciones);
            }
            else
            {
                return View(asignacion);
            }


        }
        public IActionResult EliminarAsignacion(int Idasignacion)
        {

            var oasignacion = _asignacionParqueaderoDatos.Consultar(Idasignacion);

            return View(oasignacion);
        }

        [HttpPost]
        public IActionResult EliminarAsignacion(AsignacionModel oAsignacion)
        {

            var respuesta = _asignacionParqueaderoDatos.Eliminar(oAsignacion.IdAsignacion);
            if (respuesta)
                return RedirectToAction("ListarAsignaciones");
            else
                return View();
            return View();
        }
    }
}
